using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_Attack2 : Boss_Attack {

    bool hit = false;
    float animSpeed = 1f;
    float delayAttackTimer;

    bool toFall;
    Vector3 targetPosition;

    public float delayTime = 1f;
    public float jumpVelocity = 15f;
    public float damageMultiplier = 2f;

    private void Awake()
    {
        // In case the target isnt set properly, then we manually do tis
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Animator>().SetFloat("Attack3Speed", animSpeed);

        // Our second bounding box which is a trigger
        if(toFall)
        {
            delayAttackTimer = Mathf.Max(0, delayAttackTimer - Time.deltaTime);
            if (delayAttackTimer <= 0f)
            {
                animSpeed = 1f;
                GetComponent<Rigidbody2D>().gravityScale = 1f;
                transform.position = targetPosition - new Vector3(0, 0.5f);
                if (GetComponent<Collider2D>().bounds.Intersects(target.GetComponent<Collider2D>().bounds) && !hit)
                {
                    hit = true;
                    int damage = Mathf.RoundToInt(GetComponent<Stat_AttackScript>().GetBaseAttackDamage() * damageMultiplier);
                    target.GetComponent<Stat_HealthScript>().DecreaseHealth(damage);
                    TextPopupManager.instance.ShowEnemyDamageTextPopup(GameObject.FindGameObjectWithTag("PlayerCanvas").GetComponent<Canvas>(), target.transform.position, damage);
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if(GetComponent<Rigidbody2D>().velocity.y < 0f || Mathf.Abs(transform.position.x - targetPosition.x) < 0.25f)
        {
            GetComponent<Rigidbody2D>().gravityScale = 0.01f;
            GetComponent<Rigidbody2D>().velocity = new Vector3();
            toFall = true;
        }
    }
    // This acts as a reset, this function is called when using this attack like a Start()
    public void Initiate()
    {
        toFall = false;
        targetPosition = target.transform.position;
        GetComponent<Rigidbody2D>().velocity = new Vector2(targetPosition.x - transform.position.x, jumpVelocity);
        delayAttackTimer = delayTime;
        animSpeed = 1f;
    }

    void AttackThreeStart()
    {
        animSpeed = 0f;
        hit = false;
    }

    void AttackThreeEnd()
    {
        this.enabled = false;
    }
}
