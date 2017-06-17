using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat_ExperienceScript : MonoBehaviour {

	[SerializeField]
	private float experiencePoints = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
	public void IncreaseExperience(float value) {
		experiencePoints = Mathf.Max(0, experiencePoints + value);
	}

	public void DecreaseExperience(float value) {
		experiencePoints = Mathf.Max(0, experiencePoints - value);
	}

	public float GetExperience() {
		return experiencePoints;
	}

	public void SetExperience(float value) {
		experiencePoints = Mathf.Max(0, value);
	}
}
