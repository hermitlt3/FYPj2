using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleExp : CollectibleBehavior {

	private Stat_ExperienceScript expScript;			// The exp it should have

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		expScript = GetComponent<Stat_ExperienceScript> ();
		timeToDie = false;
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();

		if (!playerGO.GetComponent<Stat_HealthScript> ().isAlive () ||
			timeToDestroy <= 0f) {

			gameObject.SetActive (false);
			// Straight add exp
			playerGO.GetComponent<Player_Experience>().IncreaseExperience(expScript.GetExperience());
		}

		if ((playerCollider.bounds.Contains (thisCollider.bounds.min) &&
		    playerCollider.bounds.Contains (thisCollider.bounds.max)) && timeToDie) {

			playerGO.GetComponent<Player_Experience> ().IncreaseExperience (expScript.GetExperience ());
			gameObject.SetActive(false);
		}
	}
}
