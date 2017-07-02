using UnityEngine;
using System.Collections;

public class Critical_SkillScript : Base_SkillScript {

	protected override void Awake() {
		base.Awake ();
		skillType = SKILL_TYPE.CRITICAL;
	}

	protected override void Start() {
		base.Start ();
	}

	protected override void  Update() {
		base.Update ();
	}
}
