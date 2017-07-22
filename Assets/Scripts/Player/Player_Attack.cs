using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour {

	Canvas playerCanvas;
	float attackRange;
	float attackSpeed;

	public LayerMask enemies;
    public GameObject[] effectPrefab;
	Animator animator;

	// Use this for initialization
	void Start () {
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
        AudioManager.instance.PlaySound(GetComponents<AudioSource>()[0]);
		Collider2D hit = Physics2D.OverlapBox(new Vector2(transform.position.x + ((GetComponent<SpriteRenderer>().flipX == true) ? 1 : -1) * attackRange * 0.5f, transform.position.y), new Vector2(attackRange, GetComponent<SpriteRenderer>().size.y), 0, enemies);

		if(hit && hit.gameObject != null) {
			hit.gameObject.SendMessage ("GetsHit", gameObject, SendMessageOptions.DontRequireReceiver);

			if ((hit.gameObject.GetComponent<EnemyAI_Logic> () || hit.gameObject.GetComponent<Boss_AI>()) && hit.gameObject.GetComponent<Stat_HealthScript>().isAlive()) {
                AudioManager.instance.PlaySound(GetComponents<AudioSource>()[2]);
                if (gotCrit) {
					TextPopupManager.instance.ShowTextPopup (playerCanvas, hit.transform.position, "-" + attackDamage.ToString (), TextPopupManager.TEXT_TYPE.CRITICAL_PLAYER);
				} else {
					TextPopupManager.instance.ShowTextPopup (playerCanvas, hit.transform.position, "-" + attackDamage.ToString (), TextPopupManager.TEXT_TYPE.DAMAGE_PLAYER);
				}

                GameObject effect = Instantiate<GameObject>(effectPrefab[Random.Range(0, effectPrefab.Length)]) as GameObject;
                effect.GetComponent<SwordEffectScript>().flipX = GetComponent<SpriteRenderer>().flipX;
                effect.transform.position = hit.transform.position;
            }
			hit.gameObject.GetComponent<Stat_HealthScript> ().DecreaseHealth (attackDamage);
		}
	}
}
