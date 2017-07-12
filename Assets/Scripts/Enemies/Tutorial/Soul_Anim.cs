using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul_Anim : Enemy_Anim {

    protected override void Awake()
    {
        animator = GetComponent<Animator>();
        healthScript = GetComponent<Stat_HealthScript>();
    }

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        Vector2 velocity = gameObject.GetComponent<Rigidbody2D>().velocity;
        velocity.y = 0;
        animator.SetFloat("Speed", velocity.magnitude);

        if (!healthScript.isAlive())
        {
            animator.SetBool("Dead", true);
        }
    }

    public override void Reset()
    {
        animator.SetBool("Dead", false);
        animator.SetTrigger("Reset");
    }
}
