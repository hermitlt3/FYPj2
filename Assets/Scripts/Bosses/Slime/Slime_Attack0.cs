using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_Attack0 : Boss_Attack {

    float animSpeed;
    public float slideSpeed;

    Vector3 startPosition;
    Vector3 endPosition;

    bool hit = false;
    bool startAnim = false;

    private void Awake()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }
        startPosition = transform.position;
        endPosition = target.transform.position;
        startAnim = false;
        animSpeed = 1f;
        slideSpeed = 10f;
    }
 
	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        GetComponent<Animator>().SetFloat("Attack1Speed", animSpeed);
        if (startAnim)
        {
            if (GetComponent<Collider2D>().bounds.Intersects(target.GetComponent<Collider2D>().bounds) && !hit)
            {
                hit = true;
                target.GetComponent<Stat_HealthScript>().DecreaseHealth(GetComponent<Stat_AttackScript>().GetBaseAttackDamage());
                TextPopupManager.instance.ShowEnemyDamageTextPopup(GameObject.FindGameObjectWithTag("PlayerCanvas").GetComponent<Canvas>(), target.transform.position, GetComponent<Stat_AttackScript>().GetBaseAttackDamage());
            }
            if (Mathf.Abs(transform.position.x - endPosition.x) < 0.5f)
            {
                animSpeed = 1f;
                GetComponent<Rigidbody2D>().velocity = new Vector3();
            }
        }
	}

    private void FixedUpdate()
    {
        if (startAnim && Mathf.Abs(transform.position.x - endPosition.x) > 0.5f)
        {
            animSpeed = 0f;
            GetComponent<Rigidbody2D>().velocity = new Vector3(((startPosition.x < endPosition.x) ? 1 : -1) * slideSpeed, 0);
        }
    }

    private void Reset()
    {
        hit = false;
        startAnim = false;
    }

    void AttackOneEnd()
    {
        Reset();
        this.enabled = false;
    }

    void AttackOneStart()
    {
        startAnim = true;
    }

    public void Initiate()
    {
        startPosition = transform.position;
        endPosition = target.transform.position;
        animSpeed = 1f;
    }
}
