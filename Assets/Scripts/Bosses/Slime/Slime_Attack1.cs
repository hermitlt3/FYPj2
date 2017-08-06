using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_Attack1 : Boss_Attack
{
    public float sinkDistance;
    public float sinkSpeed;
    public float moveSpeed = 5f;
    public float delayAttack = 2f;
    public float damageMultiplier = 2f;

    bool hit = false;
    float animSpeed = 0.25f;
    float delayAttackTimer;
    Vector3 sinkEnd;

    enum PHASE
    {
        SINK = 0,
        MOVETO,
        ATTACK
    }
    PHASE attackPhase = PHASE.SINK;

    private void Awake()
    {
        
    }

    // Use this for initialization
    void Start()
    {
        // In case the target isnt set properly, then we manually do tis
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }
        // First, set dive Anim true and disable rigidbody dynamic to enable boss to go "underground"
        // Then,  pause the animation and do the logic to unleash the kraken
        // Then,  set dive Anim to false 
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Animator>().SetFloat("Attack2Speed", animSpeed);

        switch (attackPhase)
        {
            case PHASE.SINK:
                if ((transform.position - sinkEnd).sqrMagnitude < 0.25f)
                {
                    attackPhase = PHASE.MOVETO;
                    transform.position = sinkEnd;
                    animSpeed = 0f;
                    GetComponent<Rigidbody2D>().velocity = new Vector3();
                }
                break;
            case PHASE.MOVETO:
                if (Mathf.Abs(transform.position.x - target.transform.position.x) < 0.25f)
                {
                    attackPhase = PHASE.ATTACK;
                    GetComponent<Rigidbody2D>().velocity = new Vector3();
                }
                else
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector3(((transform.position.x < target.transform.position.x) ? 1 : -1) * moveSpeed, 0);
                }
                break;
            case PHASE.ATTACK:
                delayAttackTimer = Mathf.Max(0, delayAttackTimer - Time.deltaTime);
                if (delayAttackTimer <= 0f)
                {
                    animSpeed = 1f;
                    GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                    GetComponents<Collider2D>()[1].enabled = true;
                }
                break;
        }

        // Our second bounding box which is a trigger
        if (GetComponents<Collider2D>()[1].bounds.Intersects(target.GetComponent<Collider2D>().bounds) && !hit)
        {
            hit = true;
            int damage = Mathf.RoundToInt(GetComponent<Stat_AttackScript>().GetBaseAttackDamage() * damageMultiplier);
            target.GetComponent<Stat_HealthScript>().DecreaseHealth(damage);
            TextPopupManager.instance.ShowEnemyDamageTextPopup(GameObject.FindGameObjectWithTag("PlayerCanvas").GetComponent<Canvas>(), target.transform.position, damage);
        }
    }

    private void FixedUpdate()
    {

    }
    // This acts as a reset, this function is called when using this attack like a Start()
    public void Initiate()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        attackPhase = PHASE.SINK;
        sinkEnd = transform.position - new Vector3(0, sinkDistance);
        GetComponent<Rigidbody2D>().velocity = new Vector3(0, -sinkSpeed);
        GetComponents<Collider2D>()[1].enabled = false;
        delayAttackTimer = delayAttack;
        animSpeed = 0.25f;
    }

    void AttackTwoStart()
    {
        animSpeed = 0f;
        hit = false;
    }

    void AttackTwoEnd()
    {
        GetComponents<Collider2D>()[1].enabled = false;
        this.enabled = false;
    }
}
