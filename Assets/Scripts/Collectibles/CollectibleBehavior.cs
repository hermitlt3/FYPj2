using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleBehavior : MonoBehaviour
{
    // Get player of the game
	protected GameObject playerGO;
    
    // Collider of this game object
	protected BoxCollider2D thisCollider;
    // Collider of player
	protected BoxCollider2D playerCollider;

    // Rigidbody of this game object
    protected Rigidbody2D myRigidBody;

	[SerializeField]
	protected float timeToDestroy = 3f;
	private float timeToDestroyReset;

    // Speed multiplier
    [SerializeField]
    protected float speedMultiplier = 0.0001f;

    // Maximum speed for the gameobject
    [SerializeField]
	protected float maxSpeed = 10f;

	// How long can it be invulerable
	[SerializeField]
	protected float invulerableTime = 1f;

	// Timer to check if it got stuck
	protected float stuckTime = 0f;

	// As the name suggests
	protected bool timeToDie;

	protected Vector2 direction;

	// Use this for initialization
	protected virtual void Start () {
        // Can juz pass it in but for prefab sake
        playerGO = GameObject.FindGameObjectWithTag("Player");

        thisCollider = GetComponent<BoxCollider2D>();
        playerCollider = playerGO.GetComponent<BoxCollider2D>();

        myRigidBody = GetComponent<Rigidbody2D>();

		timeToDestroyReset = timeToDestroy;
		timeToDie = false;

		direction = Vector2.zero;
	}
	
	// Update is called once per frame
	protected virtual void Update()
	{
		direction = (playerGO.transform.position - transform.position).normalized;

		// For stuck purposes
		if (myRigidBody.velocity.sqrMagnitude > 0.1f) {
			speedMultiplier = Mathf.Min(10, speedMultiplier + Time.deltaTime);
			stuckTime = 0f;
		} else {
			stuckTime += Time.deltaTime;
		} 

		if (stuckTime > 1f) {
			thisCollider.enabled = false;
			myRigidBody.gravityScale = 0f;
			myRigidBody.velocity = myRigidBody.velocity.normalized * 5f;
		}

		//////////////////////

		if (!GetComponent<SpriteRenderer> ().isVisible) {
			timeToDestroy = Mathf.Max (0, timeToDestroy - Time.deltaTime);
		} else {
			timeToDestroy = timeToDestroyReset;
		}

		invulerableTime = Mathf.Max (0, invulerableTime - Time.deltaTime);
		if (invulerableTime <= 0) {
			timeToDie = true;
		}
    }

	void FixedUpdate()
    {
		if (myRigidBody.velocity.sqrMagnitude < maxSpeed * maxSpeed)
			myRigidBody.velocity += direction * speedMultiplier;
        else
            Vector2.ClampMagnitude(myRigidBody.velocity, maxSpeed);
    }

	public void Reset() 
	{
		invulerableTime = 1f;
		timeToDie = false;
		speedMultiplier = 0.0001f;
		if (GetComponent<Collider2D> ().enabled == false) {
			GetComponent<Collider2D> ().enabled = true;
		}
		GetComponent<Rigidbody2D>().gravityScale = 2;
	}
}
