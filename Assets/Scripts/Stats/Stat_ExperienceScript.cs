using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat_ExperienceScript : MonoBehaviour {

	[SerializeField]
	private int experiencePoints = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
	public void IncreaseExperience(int value) {
		experiencePoints = Mathf.Max(0, experiencePoints + value);
	}

	public void DecreaseExperience(int value) {
		experiencePoints = Mathf.Max(0, experiencePoints - value);
	}

	public int GetExperience() {
		return experiencePoints;
	}

	public void SetExperience(int value) {
		experiencePoints = Mathf.Max(0, value);
	}
}
