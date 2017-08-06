using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_AI : Boss_AI
{

    private int numberOfAttackPatterns;
    [SerializeField]
    private float[] skillsChance;
    private float selfTimer;

    private GameObject target;
    private Animator animator;

    private void Awake()
    {
    }
    // Use this for initialization
    // Use this for initialization
    protected override void Start()
    {
        animator = GetComponent<Animator>();
        base.Start();
        numberOfAttackPatterns = 4;
        currentAttackPattern = -1;

        if (skillsChance.Length != numberOfAttackPatterns)
        {
            print("Wrong number of skills");
        }
        selfTimer = 0f;
        target = GameObject.FindGameObjectWithTag("Player");

        maxTimeBetweenIntervals = timeBetweenIntervals;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (animator.GetInteger("AttackStyle") == -1)
        {
            if (transform.position.x - target.transform.position.x > 0f && GetComponent<Stat_HealthScript>().isAlive())
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }

        if (!GetComponent<Stat_HealthScript>().isAlive())
        {
            animator.SetBool("Dead", true);
        }
        else
        {
            TimerUpdate();
            AnimationUpdate();
        }
    }

    void AnimationUpdate()
    {
        if (selfTimer >= timeBetweenIntervals)
        {
            if (!playerDependentAttacks)
            {
                float randomSkill = Random.Range(0f, 1f);
                float chosenSkill = 0f;
                float minValue = 0f;
                for (int i = 0; i < numberOfAttackPatterns; ++i)
                {
                    chosenSkill += skillsChance[i];
                    if (randomSkill >= minValue && randomSkill < chosenSkill)
                    {
                        currentAttackPattern = i;
                        break;
                    }
                    minValue += skillsChance[i];
                }
            }
            else
            {
                PlayerDependentLogic();
            }
                animator.SetInteger("AttackStyle", currentAttackPattern);
            selfTimer = 0f;
        }
    }

    void AttackUpdate()
    {
        switch (currentAttackPattern)
        {
            case 0:
                Slime_Attack0 attackOne = this.gameObject.GetComponent<Slime_Attack0>();
                attackOne.enabled = true;
                attackOne.SetTarget(target);
                attackOne.Initiate();
                break;
            case 1:
                Slime_Attack1 attackTwo = this.gameObject.GetComponent<Slime_Attack1>();
                attackTwo.enabled = true;
                attackTwo.SetTarget(target);
                attackTwo.Initiate();
                break;
            case 2:
                Slime_Attack2 attackThree = this.gameObject.GetComponent<Slime_Attack2>();
                attackThree.enabled = true;
                attackThree.SetTarget(target);
                attackThree.Initiate();
                break;
            case 3:
                Slime_Attack3 attackFour = this.gameObject.GetComponent<Slime_Attack3>();
                if(attackFour.noMoreHeals)
                {
                    currentAttackPattern = -1;
                }
                attackFour.enabled = true;
                attackFour.Initiate();
                break;
        }
        currentAttackPattern = -1;
    }

    void TimerUpdate()
    {
        selfTimer = Mathf.Min(selfTimer + Time.deltaTime, timeBetweenIntervals);
    }

    public override void Reset()
    {
        if (!gameObject.activeInHierarchy)
        {
            return;
        }
        animator.SetTrigger("Reset");
        animator.SetBool("Dead", false);
       
        shouldDie = false;
        GetComponent<Stat_HealthScript>().SetCurrentHealth(GetComponent<Stat_HealthScript>().GetMaxHealth());

        if (GetComponents<Collider2D>()[1].enabled)
        {
            GetComponents<Collider2D>()[1].enabled = false;
        }
        foreach (Boss_Attack ai in GetComponents<Boss_Attack>())
        {
            ai.enabled = false;
        }
        transform.position = new Vector3(transform.position.x, 24.3f);
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        selfTimer = 0f;
        base.Reset();
    }

    void ReleaseStuff()
    {
        if (!shouldDie)
        {
            return;
        }

        CollectiblesGenerator.instance.GenerateCollectibles(transform.position, 500, 60, 20);
        Destroy(this.gameObject.GetComponentInChildren<Canvas>().gameObject);
    }

    void ShouldDie()
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<Stat_HealthScript>().isAlive() == false)
        {
            shouldDie = false;
        }
        else
        {
            shouldDie = true;
        }
    }

    public void ResetTimer(float timer = 0f)
    {
        selfTimer = timer;
    }

    void PlayerDependentLogic()
    {
        float healthPercent = (float)GetComponent<Stat_HealthScript>().GetCurrentHealth() / (float)GetComponent<Stat_HealthScript>().GetMaxHealth();
        if (healthPercent < 0.5f && this.gameObject.GetComponent<Slime_Attack3>().noMoreHeals == false)
        {
            currentAttackPattern = 3;
            return;
        }

        if(Mathf.Abs(target.transform.position.x - transform.position.x) < 15f)
        {
            // Short ranged
            currentAttackPattern = 0;
        }
        else
        {
            currentAttackPattern = Random.Range(1, 3);
        }
    }
}
