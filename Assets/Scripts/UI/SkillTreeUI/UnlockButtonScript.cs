using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UnlockButtonScript : MonoBehaviour {

	private ToggleGroup disToggleGrp;				// Toggle group to ensure player chooses one selection only
	private Base_SkillScript skillScript;			// The script of the skill that the player has selected
	private Button disButton;						// The button of this

	private Stat_AbilityPoint playerAP;				// Player's ability points

	public SkillsInfo_Script skillsInfo;			// The skills from the skill tree are here

	public Text unlockText;

	public GameObject confirmPopup;

	// Use this for initialization
	void Start () {
		// Gets toggle group
		disToggleGrp = transform.parent.GetComponentInChildren<ToggleGroup> ();

		// If there is a selection already
		if (disToggleGrp.AnyTogglesOn ()) {
			// Gets the skill script from the selection
			skillScript = disToggleGrp.ActiveToggles ().FirstOrDefault ().transform.parent.GetComponent<Base_SkillScript> ();
		}
		// Gets the button
		disButton = GetComponent<Button> ();
		// Add listener to know if the button is clicked, and run the function
		disButton.onClick.AddListener (UnlockIsClicked);
		// Get player's ability points
		playerAP = GameObject.FindGameObjectWithTag("Player").GetComponent<Stat_AbilityPoint>();
	}

	// Update is called once per frame
	void Update () {
		// If there is a selection
		if (disToggleGrp.AnyTogglesOn ()) {
			// Gets the skill script from the selection
			skillScript = disToggleGrp.ActiveToggles ().FirstOrDefault ().transform.parent.GetComponent<Base_SkillScript>();

			// Codes to make the button interactive or not based on the skill script's unlockable variable and enuf exp bo
			if (skillScript.isUnlockable && playerAP.GetAbilityPoint() >= skillScript.GetComponent<Stat_AbilityPoint>().GetAbilityPoint()) {
				disButton.interactable = true;
				unlockText.color = new Color (unlockText.color.r, unlockText.color.g, unlockText.color.b, 1f);
			} else {
				disButton.interactable = false;
				unlockText.color = new Color (unlockText.color.r, unlockText.color.g, unlockText.color.b, 0.2f);
			}
		}
	}

	public void UnlockIsClicked() {
		confirmPopup.SetActive (true);
	}

	public void LevelUpSkill() {
		// Deduct the experience points
		playerAP.DecreaseAbilityPoint(skillScript.GetComponent<Stat_AbilityPoint> ().GetAbilityPoint ());
		// Call the function in the skill script to unlock skill
		skillScript.GetsUnlocked ();
		// Since it is upgraded already, upgrade button OFF
		disButton.interactable = false;
		// Update the skills info
		skillsInfo.UpdateSkillsInfo();
	}
}
