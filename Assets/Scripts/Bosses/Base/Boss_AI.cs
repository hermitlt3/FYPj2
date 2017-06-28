using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_AI : MonoBehaviour {

	public float maxTimeBetweenIntervals;			// The original (max) time between attacks
	public float timeBetweenIntervals	 = 5f;		// The time between attacks

	public bool incrementDifficulty = false;		// Should there be difficulty curve
	public bool playerDependentAttacks = false;		// Should the attack be RNG or strats


	public int healthPercentageDecrease = 0;		// The % of health decrease before buffing
	public float timeDecreased = 0;					// The decreased time between attacks for every health % decrease
	protected int currentAttackPattern = 0;			// Current attack of the boss

	private Stat_HealthScript healthScript;

	// Use this for initialization
	protected virtual void Start () {
		healthScript = GetComponent<Stat_HealthScript> ();
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		int healthDecreased = (healthScript.GetMaxHealth () - healthScript.GetCurrentHealth ())* 100 / healthScript.GetMaxHealth(); 
		if (incrementDifficulty) {
			timeBetweenIntervals = Mathf.Max(0.1f, maxTimeBetweenIntervals - (healthDecreased / healthPercentageDecrease) * timeDecreased);
		}
	}

	public virtual void Reset() {

	}
}
