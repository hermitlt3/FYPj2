using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_Die : MonoBehaviour {

	[SerializeField]
	protected bool youOnlyLiveOnce = false;

	protected bool shouldDie = true;

	protected Stat_ExperienceScript expOutputScript;
	// Use this for initialization
	void Start () {
		expOutputScript = GetComponent<Stat_ExperienceScript> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	protected virtual void Deactivate() {
		if (!shouldDie) {
			return;
		}
	}

	protected virtual void ShouldDie() {
		if (!GameObject.FindGameObjectWithTag ("Player").GetComponent<Stat_HealthScript> ().isAlive ()) {
			shouldDie = false;
		}
	}

	public void Reset() {
		shouldDie = true;
	}
}
