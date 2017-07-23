using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_Attack3 : Boss_Attack
{
    public int healthRestore = 200;
    public float suckSpeed = 10f;

    public GameObject[] restoreBugs;
    public bool noMoreHeals = false;

    GameObject allyTarget;
    bool hit = false;
    float animSpeed = 1f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (allyTarget)
        {
           allyTarget.GetComponent<EnemyAI_Logic>().enabled = false;
           allyTarget.GetComponent<EnemyAI_Move>().enabled = false;
           allyTarget.GetComponent<EnemyAI_Attack>().enabled = false;
        }

        if (GetComponent<Collider2D>().bounds.Intersects(allyTarget.GetComponent<Collider2D>().bounds) && !hit)
        {
            hit = true;
            GetComponent<Stat_HealthScript>().IncreaseHealth(healthRestore);
            TextPopupManager.instance.ShowTextPopup(GameObject.FindGameObjectWithTag("PlayerCanvas").GetComponent<Canvas>(), transform.position, healthRestore.ToString(), TextPopupManager.TEXT_TYPE.HEAL_ENEMY);
            allyTarget.SetActive(false);

            animSpeed = 1f;

            noMoreHeals = true;
            foreach (GameObject rb in restoreBugs)
            {
                if (rb.activeInHierarchy)
                {
                    noMoreHeals = false;
                    break;
                }
            }
            this.enabled = false;
        }
        GetComponent<Animator>().SetFloat("Attack4Speed", animSpeed);
    }

    private void FixedUpdate()
    {
        if(allyTarget)
        {
            Vector2 direction = transform.position - allyTarget.transform.position;
            allyTarget.GetComponent<Rigidbody2D>().velocity = direction.normalized * suckSpeed; 
        }
    }

    public void Initiate()
    {
        foreach(GameObject rb in restoreBugs)
        {
            if(rb.activeInHierarchy)
            {
                GetComponent<SpriteRenderer>().flipX = (rb.transform.position.x - transform.position.x < 0f) ? true : false;
                allyTarget = rb;
            }
        }
        hit = false;
        animSpeed = 1f;
    }

    void AttackFourStart()
    {
        animSpeed = 0f;
    }
}
