using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugAI_Attack : EnemyAI_Attack {

    public float rollingSpeed = 5f;
    public float jumpHeight = 10f;
    public float customRollTime = 3f;

    public int numberOfHits = 3;

    bool rollAnimationStart = false;

    float offset = 1f;
    float noCheckTime = 0.5f;
    float noCheckTimer;

    float checkStuckTime = 2f;
    float checkStuckTimer;

    float rollTimer;

    int hit = 0;

    Collider2D boundaries;
    GameObject hardCodedTargetPlayer;
    
    protected override void Awake() {
        base.Awake();
    }

    // Use this for initialization
    override protected void Start () {
        base.Start();
        boundaries = GetComponent<EnemyAI_Move>().GetMovementBoundaries();
        hardCodedTargetPlayer = GameObject.FindGameObjectWithTag("Player").gameObject;

        noCheckTimer = noCheckTime;
        checkStuckTimer = checkStuckTime;
        rollTimer = customRollTime;
    }
	
	// Update is called once per frame
	override protected void Update () {
        if (attacking)
        {
            if (rollAnimationStart)
            {
                rollTimer = Mathf.Max(0, rollTimer - Time.deltaTime);
                if (rollTimer <= 0f)
                {
                    InternalReset();
                    rollAnimationStart = false;
                }
            }
            else
            {
                if (BoxDetectedPlayer().gameObject.transform.position.x - transform.position.x < 0)
                {
                    sprite.flipX = true;
                }
                else
                {
                    sprite.flipX = false;
                }
            }
            if (GetComponent<Rigidbody2D>().velocity.x == 0)
            {
                checkStuckTimer = Mathf.Max(0, checkStuckTimer - Time.deltaTime);
                if(checkStuckTimer <= 0f)
                {
                    checkStuckTimer = checkStuckTime;
                    InternalReset();
                }
            }

            noCheckTimer = Mathf.Max(0, noCheckTimer - Time.deltaTime);
            // On ground and reach the end
            if (noCheckTimer <= 0f && (transform.position.x >= boundaries.bounds.max.x - offset || transform.position.x <= boundaries.bounds.min.x + offset))
            {
                rollAnimationStart = false;
                if(GetComponent<Rigidbody2D>().velocity.y == 0)
                {
                    InternalReset();
                }
            }
            if (hardCodedTargetPlayer.GetComponent<Collider2D>().bounds.Intersects(GetComponent<Collider2D>().bounds) && hit < numberOfHits && rollAnimationStart)
            {
                hit++;
                hardCodedTargetPlayer.GetComponent<Stat_HealthScript>().DecreaseHealth(attackDamage);
                TextPopupManager.instance.ShowTextPopup(GameObject.FindGameObjectWithTag("PlayerCanvas").GetComponent<Canvas>(), hardCodedTargetPlayer.transform.position, "-" + attackDamage.ToString(), TextPopupManager.TEXT_TYPE.DAMAGE);
            }
        }
    }

    void FixedUpdate()
    {
        if (rollAnimationStart)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(rollingSpeed * ((sprite.flipX == true) ? -1 : 1), GetComponent<Rigidbody2D>().velocity.y);
        } else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
        }
    }

    protected override bool Attack()
    {
        return true;
    }

    public override void Reset()
    {
        rollAnimationStart = false;
        hit = 0;
        noCheckTimer = noCheckTime;
    }

    protected override void GetsHit(GameObject player)
    {
        if (rollAnimationStart)
            return;

        base.GetsHit(player);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    private void RollAnimationStart()
    {
        rollAnimationStart = true;
        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
    }

    private void InternalReset()
    {
        attacking = false;
        hit = 0;
        noCheckTimer = noCheckTime;
        rollTimer = customRollTime;
    }
}
