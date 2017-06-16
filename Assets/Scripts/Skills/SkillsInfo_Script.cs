using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsInfo_Script : MonoBehaviour {

	private Critical_SkillScript critSkill;				// Critical strike chance
	private AttackDamage_SkillScript damageSkill;		// Increased damage
	private Health_SkillScript healthSkill;				// Increased health

	// Use this for initialization
	void Start () {
		// Get all skills 
		critSkill = transform.parent.GetComponentInChildren<Critical_SkillScript> ();
		damageSkill = transform.parent.GetComponentInChildren<AttackDamage_SkillScript> ();
		healthSkill = transform.parent.GetComponentInChildren<Health_SkillScript> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (critSkill.isUnlocked) {
			print ("Critical chance: " + critSkill.GetComponent<Stat_CritChance> ().GetCriticalChance ());
		} 
		if (damageSkill.isUnlocked) {
			print ("Damage increase: " + damageSkill.GetComponent<Stat_AttackScript> ().GetBaseAttackDamage ());
		}
		if (healthSkill.isUnlocked) {
			print ("Health % up: " + healthSkill.GetComponent<Stat_HealthScript> ().GetCurrentHealth ());
		}
	}

	public void UpdateSkillsInfo() {
		// Just for that slightly better performance, we call this when the button "Unlock"
		// is pressed, then since the highest level skill will be moved to the top of the hierachy
		// as the first sibling, GetComponentInChildren will automatically go for the first script found
		// aka the highest level one
		critSkill = transform.parent.GetComponentInChildren<Critical_SkillScript> ();
		damageSkill = transform.parent.GetComponentInChildren<AttackDamage_SkillScript> ();
		healthSkill = transform.parent.GetComponentInChildren<Health_SkillScript> ();
	}
}
