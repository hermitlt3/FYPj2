using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_Attack : EnemyAI_DetectPlayer {

	private float attackDamage;
	private float attackSpeed;
	//private float attackSpeedTimer; 

	private bool attacking;
	//private Animator animator;

	private bool animationEnd;
	private Canvas playerCanvas;
	// The range which enemies will detect player

	protected override void Awake() {
		base.Awake ();
	}

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		attackDamage = GetComponent<Stat_AttackScript> ().GetBaseAttackDamage ();
		attackSpeed = 1f / GetComponent<Stat_AttackSpeedScript> ().GetBaseAttackSpeed ();
		//animator = GetComponent<Animator> ();
		playerCanvas = GameObject.FindGameObjectWithTag ("PlayerCanvas").GetComponent<Canvas> ();

		//attackSpeedTimer = 1f;
		attacking = false;
		animationEnd = false;
	}

	// Update is called once per frame
	void Update () {
		//UpdateAttackSpeedTimer ();

		if (BoxDetectedPlayer () != null) {
			attacking = true;

			if (BoxDetectedPlayer ().gameObject.transform.position.x - transform.position.x < 0) {
				sprite.flipX = false;
			} else {
				sprite.flipX = true;
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

	void UpdateAttackSpeedTimer() {
		/*if (attackSpeedTimer > 0f) {
			attackSpeedTimer = Mathf.Max (0f, attackSpeedTimer - Time.deltaTime * attackSpeed);
		}
		if (attackSpeedTimer <= 0f) {
			if (Attack ()) {
				attackSpeedTimer = 1f;
			}
		}*/
	}

	bool Attack() {
		bool results = false;
		if (RayDetectedPlayer() != null) {
			if (RayDetectedPlayer ().gameObject.GetComponent<Stat_HealthScript> ().isAlive ()) {
				TextPopupManager.ShowTextPopup (playerCanvas, RayDetectedPlayer ().transform.position, "-" + attackDamage.ToString (), TextPopupManager.TEXT_TYPE.DAMAGE);
			}
			DealDamage (RayDetectedPlayer().gameObject.GetComponent<Stat_HealthScript> ());

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
}
