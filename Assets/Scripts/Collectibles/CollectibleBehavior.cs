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

	[SerializeField]
	protected float invulerableTime = 1f;
	protected bool timeToDie;

	// Use this for initialization
	protected virtual void Start () {
        // Can juz pass it in but for prefab sake
        playerGO = GameObject.FindGameObjectWithTag("Player");

        thisCollider = GetComponent<BoxCollider2D>();
        playerCollider = playerGO.GetComponent<BoxCollider2D>();

        myRigidBody = GetComponent<Rigidbody2D>();

		timeToDestroyReset = timeToDestroy;
		timeToDie = false;
	}
	
	// Update is called once per frame
	protected virtual void Update()
	{
		speedMultiplier += Time.deltaTime;

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
        if (myRigidBody.velocity.magnitude < maxSpeed)
            myRigidBody.velocity += new Vector2(playerGO.transform.position.x - transform.position.x, playerGO.transform.position.y - transform.position.y).normalized * speedMultiplier;
        else
            Vector2.ClampMagnitude(myRigidBody.velocity, maxSpeed);
    }

	public void Reset() 
	{
		invulerableTime = 1f;
		timeToDie = false;
		speedMultiplier = 1;
	}
}
