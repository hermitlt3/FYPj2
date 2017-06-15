using UnityEngine;
using System.Collections;

public class Base_SkillScript : MonoBehaviour {

	public bool isUnlockable = false;
	public bool isUnlocked = false;

	public GameObject UIToBeDisabled;

	protected Base_SkillScript previous = null;
	protected Base_SkillScript next = null;

	[SerializeField]
	protected float incrementValue = 0.5f;

	void Start() {
		previous = GetComponentInParent<Base_SkillScript> ();
		next = GetComponentInChildren<Base_SkillScript> ();

		// If it is the first level
		if (previous == null) {
			// Can be unlocked
			isUnlockable = true;
		}
	}

}
