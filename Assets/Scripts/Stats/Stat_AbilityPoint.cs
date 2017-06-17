using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat_AbilityPoint : MonoBehaviour {

	[SerializeField]
	private int abilityPoint = 0;


	[SerializeField]
	private int maxAbilityPoint = 10000; // A little measure to prevent it to go too high

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public int GetAbilityPoint() {
		return abilityPoint;
	}

	public void IncreaseAbilityPoint(int value) {
		abilityPoint = Mathf.Max(0, abilityPoint + value);
		abilityPoint = Mathf.Min(abilityPoint, maxAbilityPoint);
	}

	public void DecreaseAbilityPoint(int value) {
		abilityPoint = Mathf.Max(0, abilityPoint - value);
		abilityPoint = Mathf.Min(abilityPoint, maxAbilityPoint);
	}
}
