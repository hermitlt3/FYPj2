using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleBehavior : MonoBehaviour
{
    // Get player of the game
    GameObject playerGO;
    
    // Collider of this game object
    BoxCollider2D thisCollider;
    // Collider of player
    BoxCollider2D playerCollider;

    // Rigidbody of this game object
    Rigidbody2D myRigidBody;

    // To-do, create the game logic for player - his exp, skills, health, dead etc

    // Speed multiplier
    [SerializeField]
    protected float speedMultiplier = 1;

    // Maximum speed for the gameobject
    [SerializeField]
	protected float maxSpeed = 10f;

	// Use this for initialization
	protected virtual void Start () {
        // Can juz pass it in but for prefab sake
        playerGO = GameObject.FindGameObjectWithTag("Player");

        thisCollider = GetComponent<BoxCollider2D>();
        playerCollider = playerGO.GetComponent<BoxCollider2D>();

        myRigidBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	protected virtual void Update()
    {
        speedMultiplier += Time.deltaTime;

        if (playerCollider.bounds.Contains(thisCollider.bounds.min) &&
            playerCollider.bounds.Contains(thisCollider.bounds.max))
        {
            GameObject.Destroy(gameObject);
        }
    }

	void FixedUpdate()
    {
        if (myRigidBody.velocity.magnitude < maxSpeed)
            myRigidBody.velocity += new Vector2(playerGO.transform.position.x - transform.position.x, playerGO.transform.position.y - transform.position.y).normalized * speedMultiplier;
        else
            Vector2.ClampMagnitude(myRigidBody.velocity, maxSpeed);
    }
}
