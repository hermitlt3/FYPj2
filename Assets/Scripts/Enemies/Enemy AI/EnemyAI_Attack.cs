using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_Attack : MonoBehaviour {

	private Stat_AttackScript attackDamageScript;
	private Stat_AttackSpeedScript attackSpeedScript;
	private Stat_AttackRangeScript attackRangeScript;

	private float attackDamage;
	private float attackSpeed;
	private float attackSpeedTimer;
	private float attackRange; 


	private bool attacking;
	private GameObject target;

	// Gets the main player in our game
	protected LayerMask playerMask;
	private SpriteRenderer sprite;

	// The range which enemies will detect player

	void Awake() {
		attackDamageScript = GetComponent<Stat_AttackScript> ();
		attackSpeedScript = GetComponent<Stat_AttackSpeedScript> ();
		attackRangeScript = GetComponent<Stat_AttackRangeScript> ();
		sprite = GetComponent<SpriteRenderer> ();
		playerMask = LayerMask.GetMask ("Player");
	}

	// Use this for initialization
	void Start () {
		attackDamage = attackDamageScript.GetBaseAttackDamage ();
		attackSpeed = attackSpeedScript.GetBaseAttackSpeed ();
		attackRange = attackRangeScript.GetAttackRange ();

		attackSpeedTimer = 1f;
		attacking = false;
	}

	// Update is called once per frame
	void Update () {
		UpdateAttackSpeedTimer ();

		Collider2D collider = Physics2D.OverlapCircle (transform.position, attackRange * 2, playerMask);
		if (collider != null) {
			target = collider.gameObject;
		} else {
			attacking = false;
		}
		print (attackSpeedTimer);
		if (target) {
			if (target.transform.position.x - transform.position.x < 0) {
				sprite.flipX = false;
			} else {
				sprite.flipX = true;
			}
		} else {
			attacking = false;
		}
	}

	void FixedUpdate() {
		if (GetComponent<Rigidbody2D> ().velocity.x != 0) {
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, GetComponent<Rigidbody2D> ().velocity.y);
		}
	}

	void UpdateAttackSpeedTimer() {
		if (attackSpeedTimer > 0f) {
			attackSpeedTimer = Mathf.Max (0f, attackSpeedTimer - Time.deltaTime * attackSpeed);
		}
		if (attackSpeedTimer <= 0f) {
			if (Attack ()) {
				attackSpeedTimer = 1f;
			}
		}
	}

	bool Attack() {
		bool results = false;
		RaycastHit2D hitCollider;
		hitCollider = Physics2D.Raycast (transform.position, target.transform.position - transform.position, attackRange, playerMask);
		if (hitCollider.collider != null) {
			DealDamage (hitCollider.collider.gameObject.GetComponent<Stat_HealthScript> ());
			results = true;
		} else {
			return false;
		}
		return results;
	}

	void DealDamage(Stat_HealthScript health) {
		if (health == null) {
			return;
		}
		health.DecreaseHealth (attackDamage);
	}

	public bool isAttacking() {
		return attacking;
	}

	public void SetIsAttacking(bool value) {
		attacking = value;
	}

	public float GetAttackSpeed() {
		return attackSpeed;
	}

	public void Reset() {
		attackSpeedTimer = 1f;
	}
}
