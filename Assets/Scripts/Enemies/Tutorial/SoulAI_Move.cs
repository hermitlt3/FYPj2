using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulAI_Move : EnemyAI_Move {

    protected override void Awake()
    {
        areaBounds = areaOfAllowedMovement.gameObject.GetComponent<Collider2D>();
		myRigidbody = GetComponent<Rigidbody2D> ();
        movementSpeed = GetComponent<Stat_MovementSpdScript>().GetBaseMS();
        sprite = GetComponent<SpriteRenderer>();
        thisCollider = GetComponent<BoxCollider2D>();
    }

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        if (sprite.flipX)
        {
            myRigidbody.velocity = new Vector2(-movementSpeed, myRigidbody.velocity.y);
            if (transform.position.x < areaBounds.bounds.min.x)
            {
                sprite.flipX = false;
            }
        }
        else
        {
            myRigidbody.velocity = new Vector2(movementSpeed, myRigidbody.velocity.y);
            if (transform.position.x > areaBounds.bounds.max.x)
            {
                sprite.flipX = true;
            }
        }
    }
}
