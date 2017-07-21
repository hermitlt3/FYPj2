using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugAI_Attack : EnemyAI_Attack {

    public float rollingSpeed = 5f;

    bool rollAnimationStart = false;
    float sidesOffset = 0.5f;

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
    }
	
	// Update is called once per frame
	override protected void Update () {
        if (attacking)
        {
            if (transform.position.x > boundaries.bounds.max.x - sidesOffset || transform.position.x < boundaries.bounds.min.x + sidesOffset)
            {
                attacking = false;
            }
            if (hardCodedTargetPlayer.GetComponent<Collider2D>().bounds.Intersects(GetComponent<Collider2D>().bounds))
            {
                attacking = false;
                rollAnimationStart = false;
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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    private void RollAnimationStart()
    {
        rollAnimationStart = true;
        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 10);
    }
}
