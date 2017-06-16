using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UnlockButtonScript : MonoBehaviour {

	private ToggleGroup disToggleGrp;				// Toggle group to ensure player chooses one selection only
	private Base_SkillScript skillScript;			// The script of the skill that the player has selected
	private Button disButton;						// The button of this

	public SkillsInfo_Script skillsInfo;

	// DEBUG PURPOSES
	public int expTest = 100;

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
	}

	// Update is called once per frame
	void Update () {
		// If there is a selection
		if (disToggleGrp.AnyTogglesOn ()) {
			// Gets the skill script from the selection
			skillScript = disToggleGrp.ActiveToggles ().FirstOrDefault ().transform.parent.GetComponent<Base_SkillScript>();

			// Codes to make the button interactive or not based on the skill script's unlockable variable and enuf exp bo
			if (skillScript.isUnlockable && expTest >= skillScript.GetComponent<Stat_ExperienceScript> ().GetExperience ()) {
				disButton.interactable = true;
			} else {
				disButton.interactable = false;
			}
		}
	}

	public void UnlockIsClicked() {
		// Deduct the experience points
		expTest -= skillScript.GetComponent<Stat_ExperienceScript> ().GetExperience ();
		// Call the function in the skill script to unlock skill
		skillScript.GetsUnlocked ();
		// Since it is upgraded already, upgrade button OFF
		disButton.interactable = false;
		// Update the skills info
		skillsInfo.UpdateSkillsInfo();
	}
}
