using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (EnemyAI_DetectPlayer))]
public class EnemyAI_Move : EnemyAI_DetectPlayer {

	private Stat_MovementSpdScript movementSpeedScript;

	// The area where the enemy is allowed to be
	[SerializeField]
	protected Transform areaOfAllowedMovement;
	private BoxCollider2D areaBounds;
	private float movementSpeed;
	private bool moving;

	// How enemies react towards player
	[SerializeField]
	protected enum AGGRESSION_TYPE {
		AGGRESSIVE,
		PASSIVE // non-aggressive
	} 
	[SerializeField]
	protected AGGRESSION_TYPE aggressionType;


	protected override void Awake () {
		base.Awake ();
		areaBounds = areaOfAllowedMovement.gameObject.GetComponent<BoxCollider2D> ();
		movementSpeed = GetComponent<Stat_MovementSpdScript> ().GetBaseMS();
	}

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		moving = true;
	}

	// Update is called once per frame
	void Update () {
		if (!sprite.flipX) {
			myRigidbody.velocity = new Vector2 (-movementSpeed, myRigidbody.velocity.y);
			if (transform.position.x < areaBounds.bounds.min.x) {
				sprite.flipX = true;
			}
		} else {
			myRigidbody.velocity = new Vector2 (movementSpeed, myRigidbody.velocity.y);
			if (transform.position.x + transform.lossyScale.x > areaBounds.bounds.max.x) {
				sprite.flipX = false;
			}
		}

		if (aggressionType == AGGRESSION_TYPE.AGGRESSIVE) {
			if (RayDetectedPlayer ()) {
				moving = false;
			}
		}
	}

	public bool isMoving() {
		return moving;
	}

	public void SetIsMoving(bool value) {
		moving = value;
	}

	void GetsHit(GameObject gameObject) {
		if (aggressionType == AGGRESSION_TYPE.PASSIVE) {
			aggressionType = AGGRESSION_TYPE.AGGRESSIVE;
		}
	}
}
