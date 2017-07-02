using UnityEngine;
using System.Collections;

public class Base_SkillScript : MonoBehaviour {

	public bool isUnlockable = false;						// If skill can be unlocked
	public bool isUnlocked = false;							// If skill has been unlocked

	public GameObject UIToBeDisabledWhenUnlockable;			// As the name suggests. Can use getChild(index) since its fixed but meh
	public GameObject UIToBeDisabledWhenUnlocked;			// As the name suggests. Can use getChild(index) since its fixed but meh

	protected Base_SkillScript previous = null;				// The previous level
	protected Base_SkillScript next = null;					// The next level

	// JUST A DEBUG
	public bool getsUnlocked = false;

	public enum SKILL_TYPE
	{
		CRITICAL = 0,
		ATTACK_DAMAGE,
		ATTACK_SPEED,
		HEALTH
	}
	public SKILL_TYPE skillType;
	public int skillLevel;

	protected virtual void Awake() {
		skillLevel = transform.GetSiblingIndex () + 1;
	}

	protected virtual void Start() {

		// If its not the first level
		if (transform.GetSiblingIndex () > 0) {
			// Get the reference of the previous level from sibling hierachy
			previous = transform.parent.GetChild (transform.GetSiblingIndex () - 1).GetComponent<Base_SkillScript> ();
		}
		// If its not the last level
		if (transform.GetSiblingIndex () < transform.parent.childCount - 1) {
			// Get the reference of the next level from sibling hierachy
			next = transform.parent.GetChild (transform.GetSiblingIndex () + 1).GetComponent<Base_SkillScript> ();
		}
		// If it is the first level
		if (previous == null) {
			// Can be unlocked
			isUnlockable = true;
		}
	}

	protected virtual void Update() {

		// FOR DEBUG PURPOSES
		if (getsUnlocked) {
			GetsUnlocked ();
		}
		// UI things to make image disappear when its unlockable
		if (isUnlockable) {
			UIToBeDisabledWhenUnlockable.SetActive (false);
		}
	}

	// Function called by the Unlock button
	public void GetsUnlocked() {
		// THE DEBUG
		getsUnlocked = false;

		// Not unlockable anymore since its already unlocked
		isUnlockable = false;
		// Like I said, its already unlocked
		isUnlocked = true;

		// Bring it to the top of the hierachy for the logic whereby some other script 
		// gets the crit script from the top of hierachy and pass it to the player
		transform.SetAsFirstSibling ();

		// Same UI things to make image disappear when its unlocked
		UIToBeDisabledWhenUnlocked.SetActive (false);

		// If there is a next level
		if (next) {
			// It can be unlocked now. Simple!
			next.isUnlockable = true;
		}
	}
}
