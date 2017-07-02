using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeed_SkillScript : Base_SkillScript {

	protected override void Awake() {
		base.Awake ();
		skillType = SKILL_TYPE.ATTACK_SPEED;
	}

	// Use this for initialization
	protected override void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
	}
}
