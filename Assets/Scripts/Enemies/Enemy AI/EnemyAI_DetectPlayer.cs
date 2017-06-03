using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_DetectPlayer : MonoBehaviour {

	protected LayerMask playerMask;
	protected Rigidbody2D myRigidbody;
	protected float attackRange;
	protected SpriteRenderer sprite;

	protected virtual void Awake () {
		myRigidbody = GetComponent<Rigidbody2D> ();
		attackRange = GetComponent<Stat_AttackRangeScript>().GetAttackRange ();
		sprite = GetComponent<SpriteRenderer> ();
	}

	// Use this for initialization
	protected virtual void Start () {
		playerMask = LayerMask.GetMask ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	protected Collider2D RayDetectedPlayer() {
		RaycastHit2D hit;
		if (myRigidbody.velocity.magnitude != 0) {
			hit = Physics2D.Raycast (transform.position, new Vector2 (Mathf.Clamp (myRigidbody.velocity.x, -1, 1), 0), attackRange, playerMask);
			return hit.collider;
		} else {
			hit = Physics2D.Raycast (transform.position, new Vector3(((sprite.flipX == false) ? -1 : 1), transform.position.y), attackRange, playerMask);
			return hit.collider;
		}
	}

	protected Collider2D BoxDetectedPlayer() {
		Collider2D collider = Physics2D.OverlapCircle (transform.position, attackRange, playerMask);
		return collider;
	}

	void GetsHit(GameObject player) {
		float xDiff = player.transform.position.x - transform.position.x;
		if (xDiff < 0)
			sprite.flipX = false;
		else
			sprite.flipX = true;
	}
}
