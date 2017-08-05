using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillUIScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	public GameObject description;

	public int flipX, flipY;

	private RectTransform rectTransform;
	private Base_SkillScript theSkill;

	string levelText, descText;

	// Use this for initialization
	void Start () {
		rectTransform = GetComponent<RectTransform> ();
		theSkill = transform.parent.GetComponent<Base_SkillScript> ();


		switch (theSkill.skillType) {
		case Base_SkillScript.SKILL_TYPE.ATTACK_DAMAGE:
			levelText = "Damage level: " + theSkill.skillLevel;
			descText = "Damage increased by: " + theSkill.GetComponent<Stat_AttackScript> ().GetBaseAttackDamage ();
			description.GetComponent<SkillDescriptionScript> ().ChangeDescription (levelText, descText);

			break;
		case Base_SkillScript.SKILL_TYPE.ATTACK_SPEED:
			levelText = "Speed level: " + theSkill.skillLevel;
			descText = "Attack Speed increased by: " + theSkill.GetComponent<Stat_AttackSpeedScript> ().GetAttackSpeed () * 100 + "%";
			description.GetComponent<SkillDescriptionScript> ().ChangeDescription (levelText, descText);

			break;
		case Base_SkillScript.SKILL_TYPE.CRITICAL:
			levelText = "Critical level: " + theSkill.skillLevel;
			descText = "Critical chance increased by: " + theSkill.GetComponent<Stat_CritChance> ().GetCriticalChance () + "%";
			description.GetComponent<SkillDescriptionScript> ().ChangeDescription (levelText, descText);

			break;
		case Base_SkillScript.SKILL_TYPE.HEALTH:
			levelText = "Health level: " + theSkill.skillLevel;
			descText = "Health increased by: " + theSkill.GetComponent<Stat_HealthScript> ().GetMaxHealth ();
			description.GetComponent<SkillDescriptionScript> ().ChangeDescription (levelText, descText);

			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnPointerEnter(PointerEventData eventData) {
		description.GetComponent<SkillDescriptionScript> ().ChangeDescription (levelText, descText);
		description.GetComponent<SkillDescriptionScript> ().ShowDescription (rectTransform, flipX, flipY);
	}

	public void OnPointerExit(PointerEventData eventData) {
		description.GetComponent<SkillDescriptionScript> ().HideDescription ();
	}
}
