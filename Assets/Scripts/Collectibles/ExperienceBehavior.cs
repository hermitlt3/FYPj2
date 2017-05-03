using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceBehavior : MonoBehaviour {

    // Get player of the game
    GameObject player;
    
    // Collider of this game object
    BoxCollider2D thisCollider;
    // Collider of player
    BoxCollider2D playerCollider;

    // Rigidbody of this game object
    Rigidbody2D myRigidBody;

    // Speed multiplier
    [SerializeField]
    float speedMultiplier = 1;

    // Time before going to player
    [SerializeField]
    float myTimer = 1f;

    // Maximum speed for the gameobject
    [SerializeField]
    float maxSpeed = 20f;

	// Use this for initialization
	void Start () {
        // Can juz pass it in but for prefab sake
        player = GameObject.FindGameObjectWithTag("Player");

        thisCollider = GetComponent<BoxCollider2D>();
        playerCollider = player.GetComponent<BoxCollider2D>();

        myRigidBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
    void Update()
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
            myRigidBody.velocity += new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y).normalized * speedMultiplier;
        else
            Vector2.ClampMagnitude(myRigidBody.velocity, maxSpeed);
    }
}
