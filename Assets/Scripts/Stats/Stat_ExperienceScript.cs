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

	public void AddExperience(int exp) {
		experiencePoints = exp;
	}

	public int GetExperience() {
		return experiencePoints;
	}
}
