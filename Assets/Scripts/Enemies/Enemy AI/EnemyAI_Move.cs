using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_Move : EnemyAI_DetectPlayer {

	protected Stat_MovementSpdScript movementSpeedScript;

	// The area where the enemy is allowed to be
	[SerializeField]
	protected Transform areaOfAllowedMovement;
	protected Collider2D areaBounds;
	protected float movementSpeed;
	protected bool moving;

	// How enemies react towards player
	[SerializeField]
	protected enum AGGRESSION_TYPE {
		AGGRESSIVE,
		PASSIVE // non-aggressive
	} 
	[SerializeField]
	protected AGGRESSION_TYPE aggressionType;
	protected AGGRESSION_TYPE originalAggression;

	protected override void Awake () {
		base.Awake ();
		areaBounds = areaOfAllowedMovement.gameObject.GetComponent<Collider2D> ();
		movementSpeed = GetComponent<Stat_MovementSpdScript> ().GetBaseMS();
		originalAggression = aggressionType;
	}

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		moving = true;
	}

	// Update is called once per frame
	void Update () {
		if (sprite.flipX) {
			myRigidbody.velocity = new Vector2 (-movementSpeed, myRigidbody.velocity.y);
			if (transform.position.x < areaBounds.bounds.min.x) {
				sprite.flipX = false;
			}
		} else {
			myRigidbody.velocity = new Vector2 (movementSpeed, myRigidbody.velocity.y);
			if (transform.position.x > areaBounds.bounds.max.x) {
				sprite.flipX = true;
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

	public void Reset() {
		aggressionType = originalAggression;
	}

	void GetsHit(GameObject gameObject) {
		if (aggressionType == AGGRESSION_TYPE.PASSIVE) {
			aggressionType = AGGRESSION_TYPE.AGGRESSIVE;
		}
	}
}
