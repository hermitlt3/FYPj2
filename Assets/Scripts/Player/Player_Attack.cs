using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour {

	Controller2D controller;
	Canvas playerCanvas;
	float attackRange;
	float attackSpeed;

	public LayerMask enemies;
	Animator animator;

	// Use this for initialization
	void Start () {
		controller = GetComponent<Controller2D> ();
		animator = GetComponent<Animator> ();
		playerCanvas = GameObject.FindGameObjectWithTag ("PlayerCanvas").GetComponent<Canvas> ();
		attackRange = GetComponent<Stat_AttackRangeScript> ().GetAttackRange ();
		attackSpeed = GetComponent<Stat_AttackSpeedScript> ().GetAttackSpeed ();
	}
	
	// Update is called once per frame
	void Update () {
		animator.SetFloat ("AttackSpeed", attackSpeed + GetComponent<Stat_AttackSpeedScript> ().bonusAttackSpeed);

	}

	void Attack() {
		int attackDamage = GetComponent<Stat_AttackScript> ().GetBaseAttackDamage () + GetComponent<Stat_AttackScript> ().bonusAttack;
		float isCrit = 0f;
		bool gotCrit = false;
		if (GetComponent<Stat_CritChance> ()) {
			isCrit = GetComponent<Stat_CritChance> ().GetCriticalChance ();
			gotCrit = (isCrit >= Random.Range (0, 100)) ? true : false;
		}
		if (gotCrit) {
			attackDamage *= 2;
		}
		Collider2D hit = Physics2D.OverlapBox(new Vector2(transform.position.x + Mathf.Clamp (controller.collisions.faceDir, -1, 1) * attackRange / 2, transform.position.y), new Vector2(attackRange, GetComponent<SpriteRenderer>().size.y), 0, enemies);

		if(hit && hit.gameObject != null) {
			hit.gameObject.SendMessage ("GetsHit", gameObject, SendMessageOptions.DontRequireReceiver);
	
			if ((hit.gameObject.GetComponent<EnemyAI_Logic> () || hit.gameObject.GetComponent<Boss_AI>()) && hit.gameObject.GetComponent<Stat_HealthScript>().isAlive()) {
				if (gotCrit) {
					TextPopupManager.instance.ShowTextPopup (playerCanvas, hit.transform.position, "-" + attackDamage.ToString (), TextPopupManager.TEXT_TYPE.CRITICAL);
				} else {
					TextPopupManager.instance.ShowTextPopup (playerCanvas, hit.transform.position, "-" + attackDamage.ToString (), TextPopupManager.TEXT_TYPE.DAMAGE);
				}
			}
			hit.gameObject.GetComponent<Stat_HealthScript> ().DecreaseHealth (attackDamage);
		}
	}
}
