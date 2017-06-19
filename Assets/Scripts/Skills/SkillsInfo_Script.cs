using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsInfo_Script : MonoBehaviour {

	private GameObject player;							// PLAYAAAAAAA

	private Critical_SkillScript critSkill;				// Critical strike chance
	private AttackDamage_SkillScript damageSkill;		// Increased damage
	private Health_SkillScript healthSkill;				// Increased health
	private AttackSpeed_SkillScript attSpdSkill;		// Increased attack speed

	// Use this for initialization
	void Start () {
		// Get all skills 
		critSkill = transform.parent.GetComponentInChildren<Critical_SkillScript> ();
		damageSkill = transform.parent.GetComponentInChildren<AttackDamage_SkillScript> ();
		healthSkill = transform.parent.GetComponentInChildren<Health_SkillScript> ();
		attSpdSkill = transform.parent.GetComponentInChildren<AttackSpeed_SkillScript> ();

		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void UpdateSkillsInfo() {
		// Just for that slightly better performance, we call this when the button "Unlock"
		// is pressed, then since the highest level skill will be moved to the top of the hierachy
		// as the first sibling, GetComponentInChildren will automatically go for the first script found
		// aka the highest level one

		// Crit chance
		critSkill = transform.parent.GetComponentInChildren<Critical_SkillScript> ();

		// Damage
		damageSkill = transform.parent.GetComponentInChildren<AttackDamage_SkillScript> ();

		// Health
		healthSkill = transform.parent.GetComponentInChildren<Health_SkillScript> ();

		// Attack speed
		attSpdSkill = transform.parent.GetComponentInChildren<AttackSpeed_SkillScript> ();
	

		UpdatePlayer ();
	}

	private void UpdatePlayer() {
		if (!player) {
			return;
		}

		UpdateCriticalChance ();
		UpdateAttackDamage ();
		UpdateHealth ();
		UpdateAttackSpeed ();
	}

	void UpdateCriticalChance() {
		if (!critSkill.isUnlocked) {
			return;
		}
		if (player.GetComponent<Stat_CritChance> ()) {
			player.GetComponent<Stat_CritChance> ().SetCriticalChance (critSkill.GetComponent<Stat_CritChance> ().GetCriticalChance ());
		} else {
			Stat_CritChance crit = player.AddComponent<Stat_CritChance> ();
			crit.SetCriticalChance (critSkill.GetComponent<Stat_CritChance> ().GetCriticalChance ());
		}
	}

	void UpdateAttackDamage() {
		if (!damageSkill.isUnlocked) {
			return;
		}
		// Player originally has this 
		player.GetComponent<Stat_AttackScript> ().bonusAttack  =  damageSkill.GetComponent<Stat_AttackScript> ().GetBaseAttackDamage ();
	}

	void UpdateHealth() {
		if (!healthSkill.isUnlocked) {
			return;
		}
		player.GetComponent<Stat_HealthScript> ().bonusHealth = player.GetComponent<Stat_HealthScript> ().GetMaxHealth() * healthSkill.GetComponent<Stat_HealthScript> ().GetCurrentHealth() / 100;
	}

	void UpdateAttackSpeed() {
		if (!attSpdSkill.isUnlocked) {
			return;
		}
		player.GetComponent<Stat_AttackSpeedScript> ().bonusAttackSpeed = player.GetComponent<Stat_AttackSpeedScript> ().GetAttackSpeed() * attSpdSkill.GetComponent<Stat_AttackSpeedScript> ().GetAttackSpeed();
	}
}
