using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_Move : MonoBehaviour {

	private Rigidbody2D myRigidbody;
	private Stat_MovementSpdScript movementSpeedScript;
	private Stat_AttackRangeScript attackRangeScript;

	// The area where the enemy is allowed to be
	[SerializeField]
	protected Transform areaOfAllowedMovement;
	private BoxCollider2D areaBounds;
	private SpriteRenderer sprite;

	private bool moving;
	private float attackRange;
	private LayerMask playerMask;

	// How enemies react towards player
	[SerializeField]
	protected enum AGGRESSION_TYPE {
		AGGRESSIVE,
		PASSIVE // non-aggressive
	} 
	[SerializeField]
	protected AGGRESSION_TYPE aggressionType;


	void Awake () {
		myRigidbody = GetComponent<Rigidbody2D> ();
		movementSpeedScript = GetComponent<Stat_MovementSpdScript> ();
		attackRangeScript = GetComponent<Stat_AttackRangeScript> ();
		areaBounds = areaOfAllowedMovement.gameObject.GetComponent<BoxCollider2D> ();
		sprite = GetComponent<SpriteRenderer> ();
		playerMask = LayerMask.GetMask ("Player");
	}

	// Use this for initialization
	void Start () {
		moving = true;
		attackRange = attackRangeScript.GetAttackRange ();
	}

	// Update is called once per frame
	void Update () {
		if (!sprite.flipX) {
			myRigidbody.velocity = new Vector2 (-movementSpeedScript.GetBaseMS (), myRigidbody.velocity.y);
			if (transform.position.x < areaBounds.bounds.min.x) {
				sprite.flipX = true;
			}
		} else {
			myRigidbody.velocity = new Vector2 (movementSpeedScript.GetBaseMS (), myRigidbody.velocity.y);
			if (transform.position.x + transform.lossyScale.x > areaBounds.bounds.max.x) {
				sprite.flipX = false;
			}
		}

		if (aggressionType == AGGRESSION_TYPE.AGGRESSIVE) {
			RaycastHit2D hit;
			hit = Physics2D.Raycast (transform.position, new Vector2(Mathf.Clamp(myRigidbody.velocity.x, -1, 1), 0), attackRange, playerMask);
			if (hit.collider != null) {
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
}
