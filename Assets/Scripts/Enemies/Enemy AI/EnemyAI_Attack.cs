using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_Attack : EnemyAI_DetectPlayer {

	protected int attackDamage;
	protected float attackSpeed;

	protected bool attacking;

	protected bool animationEnd;
	protected Canvas playerCanvas;

	protected override void Awake() {
		base.Awake ();
	}

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		attackDamage = GetComponent<Stat_AttackScript> ().GetBaseAttackDamage ();
		attackSpeed = 1f / GetComponent<Stat_AttackSpeedScript> ().GetAttackSpeed ();
		playerCanvas = GameObject.FindGameObjectWithTag ("PlayerCanvas").GetComponent<Canvas> ();

		attacking = false;
		animationEnd = false;
	}

	// Update is called once per frame
	protected virtual void Update () {

		if (BoxDetectedPlayer () != null) {
			attacking = true;

			if (BoxDetectedPlayer ().gameObject.transform.position.x - transform.position.x < 0) {
				sprite.flipX = true;
			} else {
				sprite.flipX = false;
			}

		} else {
			if (animationEnd) {
				attacking = false;
				animationEnd = false;
			}
		}

		animationEnd = false;
	}

	void FixedUpdate() {
		if (GetComponent<Rigidbody2D> ().velocity.x != 0) {
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, GetComponent<Rigidbody2D> ().velocity.y);
		}
	}

	protected virtual bool Attack() {
		bool results = false;
		if (RayDetectedPlayer() != null) {
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

	void AnimationEnds() {
		animationEnd = true;
	}

	public virtual void Reset() {

	}
}
