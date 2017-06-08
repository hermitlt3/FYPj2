using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleExp : CollectibleBehavior {

	// Use this for initialization
	protected override void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();

		if (!playerGO.GetComponent<Stat_HealthScript> ().isAlive () ||
			timeToDestroy <= 0f) {

			gameObject.SetActive (false);
			// Straight add exp
		}

		if (playerCollider.bounds.Contains(thisCollider.bounds.min) &&
			playerCollider.bounds.Contains(thisCollider.bounds.max))
		{
			gameObject.SetActive(false);
		}
	}
}
