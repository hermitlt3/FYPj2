using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugAI_Attack : EnemyAI_Attack {

    public float rollingSpeed = 5f;
    public float jumpHeight = 10f;
    public float customRollTime = 3f;

    public int numberOfHits = 3;

    bool rollAnimationStart = false;
    bool onLeftmostSide = false;
    bool onRightmostSide = false;

    float noCheckTime = 0.5f;
    float noCheckTimer;

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
                if (BoxDetectedPlayer().gameObject && BoxDetectedPlayer().gameObject.transform.position.x - transform.position.x < 0f && !onLeftmostSide)
                {
                    sprite.flipX = true;
                }
                else if (BoxDetectedPlayer().gameObject && BoxDetectedPlayer().gameObject.transform.position.x - transform.position.x >= 0f && !onRightmostSide)
                {
                    sprite.flipX = false;
                }
            }

            noCheckTimer = Mathf.Max(0, noCheckTimer - Time.deltaTime);

            if (transform.position.x > boundaries.bounds.max.x)
            {
                onRightmostSide = true;
                onLeftmostSide = false;
                transform.position = new Vector3(boundaries.bounds.max.x, transform.position.y);
            }
            if (transform.position.x < boundaries.bounds.min.x)
            {
                onRightmostSide = false;
                onLeftmostSide = true;
                transform.position = new Vector3(boundaries.bounds.min.x, transform.position.y);
            }
            
            if (hardCodedTargetPlayer.GetComponent<Collider2D>().bounds.Intersects(GetComponent<Collider2D>().bounds) && hit < numberOfHits && rollAnimationStart)
            {
                hit++;
                hardCodedTargetPlayer.GetComponent<Stat_HealthScript>().DecreaseHealth(attackDamage);
                TextPopupManager.instance.ShowTextPopup(GameObject.FindGameObjectWithTag("PlayerCanvas").GetComponent<Canvas>(), hardCodedTargetPlayer.transform.position, "-" + attackDamage.ToString(), TextPopupManager.TEXT_TYPE.DAMAGE_ENEMY);
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
