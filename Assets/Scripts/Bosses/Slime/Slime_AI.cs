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
        animator = GetComponent<Animator>();
    }
    // Use this for initialization
    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        numberOfAttackPatterns = 2;
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
            if (transform.position.x - target.transform.position.x > 0f)
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
            animator.SetInteger("AttackStyle", currentAttackPattern);
            selfTimer = 0f;
        }
    }

    void AttackUpdate()
    {
        if (playerDependentAttacks)
        {

        }
        else
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
            }
            currentAttackPattern = -1;
        }
    }

    void TimerUpdate()
    {
        selfTimer = Mathf.Min(selfTimer + Time.deltaTime, timeBetweenIntervals);
    }

    public override void Reset()
    {
        if (!this.gameObject.activeInHierarchy)
        {
            return;
        }
        animator.SetTrigger("Reset");
        animator.SetBool("Dead", false);
        for (int i = 0; i < transform.childCount; ++i)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        shouldDie = false;
        GetComponent<Stat_HealthScript>().SetCurrentHealth(GetComponent<Stat_HealthScript>().GetMaxHealth());
    }

    void ReleaseStuff()
    {
        if (!shouldDie)
        {
            return;
        }

        CollectiblesGenerator.instance.GenerateCollectibles(transform.position, 100, 20);
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
}
