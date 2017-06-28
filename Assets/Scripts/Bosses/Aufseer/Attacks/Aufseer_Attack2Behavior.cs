using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aufseer_Attack2Behavior : MonoBehaviour {

	private Stat_AttackScript attackDamage;
	private bool hasHit;
	private bool toHit;
	// Use this for initialization
	void Start () {
		attackDamage = GetComponent<Stat_AttackScript> ();
		hasHit = false;
		toHit = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag("Player") && !hasHit && toHit) {
			other.gameObject.GetComponent<Stat_HealthScript> ().DecreaseHealth (attackDamage.GetBaseAttackDamage ());
			TextPopupManager.instance.ShowDamageTextPopup (GameObject.FindGameObjectWithTag ("PlayerCanvas").GetComponent<Canvas>(), other.transform.position, attackDamage.GetBaseAttackDamage());
			hasHit = true;
		}
	}

	void OnTriggerStay2D(Collider2D other) {
		if (other.gameObject.CompareTag("Player") && !hasHit && toHit) {
			other.gameObject.GetComponent<Stat_HealthScript> ().DecreaseHealth (attackDamage.GetBaseAttackDamage ());
			TextPopupManager.instance.ShowDamageTextPopup (GameObject.FindGameObjectWithTag ("PlayerCanvas").GetComponent<Canvas>(), other.transform.position, attackDamage.GetBaseAttackDamage());
			hasHit = true;
		}
	}

	void AnimationStart() {
		toHit = true;
	}

	void DamageEnd() {
		toHit = false;
	}

	void AnimationEnd() {
		this.gameObject.SetActive (false);
	}
		
	public void Reset() {
		hasHit = false;
		toHit = false;
	}
}
