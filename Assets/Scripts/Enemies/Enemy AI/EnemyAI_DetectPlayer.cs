using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_DetectPlayer : MonoBehaviour {

	protected LayerMask playerMask;
	protected Rigidbody2D myRigidbody;
	protected float attackRange;
	protected SpriteRenderer sprite;
	protected BoxCollider2D thisCollider;

	protected virtual void Awake () {
		myRigidbody = GetComponent<Rigidbody2D> ();
		attackRange = GetComponent<Stat_AttackRangeScript>().GetAttackRange ();
		sprite = GetComponent<SpriteRenderer> ();
		thisCollider = GetComponent<BoxCollider2D> ();
	}

	// Use this for initialization
	protected virtual void Start () {
		playerMask = LayerMask.GetMask ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (myRigidbody.velocity.magnitude > 1) {
			Debug.DrawLine(transform.position, transform.position + new Vector3 (Mathf.Clamp (myRigidbody.velocity.x, -1, 1) * attackRange, 0));
		} else {
			Debug.DrawLine(transform.position, transform.position + new Vector3 (((sprite.flipX == false) ? 1 : -1) * attackRange, 0));
		}
	}

	protected Collider2D RayDetectedPlayer() {
		RaycastHit2D hit;// = Physics2D.Raycast (transform.position, new Vector2 (Mathf.Clamp (myRigidbody.velocity.x, -1, 1), 0), attackRange, playerMask);
		Collider2D[] hitCollider = new Collider2D[2];
		Vector2 size = Vector2.zero;//GetComponent<SpriteRenderer>().size;

		for (int i = -1; i <= 1; i += 2) {
			if (myRigidbody.velocity.magnitude > 1) {
				hit = Physics2D.Raycast (new Vector2 (transform.position.x, transform.position.y - i * size.y), new Vector2 (Mathf.Clamp (myRigidbody.velocity.x, -1, 1), 0), attackRange, playerMask);

				hitCollider [Mathf.Clamp(i, 0, 1)] = hit.collider;

			} else {
				hit = Physics2D.Raycast (new Vector2 (transform.position.x, transform.position.y - i * size.y), new Vector2 (((sprite.flipX == false) ? 1 : -1), 0), attackRange, playerMask);
				hitCollider [Mathf.Clamp(i, 0, 1)] = hit.collider;
			}
		}
		for (int i = 0; i < 1; i++) {
			if (hitCollider [i] != null)
				return hitCollider [i];
		}
		return hitCollider [0];
	}

	protected Collider2D BoxDetectedPlayer() {
		Collider2D collider = Physics2D.OverlapCircle (transform.position, attackRange, playerMask);
		return collider;
	}

	protected Collider2D RayDetectLayer(LayerMask layer) {
		RaycastHit2D hit;
		if (myRigidbody.velocity.magnitude > 1) {
			hit = Physics2D.Raycast (transform.position, new Vector2 (Mathf.Clamp (myRigidbody.velocity.x, -1, 1), 0), attackRange, layer);
			return hit.collider;
		} else {
			hit = Physics2D.Raycast (transform.position, new Vector2(((sprite.flipX == false) ? 1 : -1), 0), attackRange, layer);
			return hit.collider;
		}
	}

	void GetsHit(GameObject player) {
		float xDiff = player.transform.position.x - transform.position.x;
		if (xDiff < 0)
			sprite.flipX = true;
		else
			sprite.flipX = false;
	}

	void OnDrawGizmos() {
		
	}
}
