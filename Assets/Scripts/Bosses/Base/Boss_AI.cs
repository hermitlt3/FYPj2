using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_AI : MonoBehaviour {

	[HideInInspector]
	public float maxTimeBetweenIntervals = 5f;
	[HideInInspector]
	public float timeBetweenIntervals = 5f;
	[HideInInspector]
	public bool incrementDifficulty = true;
	[HideInInspector]
	public int healthPercentageDecrease = 0;
	[HideInInspector]
	public bool playerDependentAttacks = false;

	protected int currentAttackPattern = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public virtual void Reset() {

	}
}
