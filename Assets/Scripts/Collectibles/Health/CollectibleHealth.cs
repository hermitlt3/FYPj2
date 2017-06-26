using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleHealth : CollectibleBehavior {

	private Stat_HealthScript healthScript;			// The exp it should have

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		healthScript = GetComponent<Stat_HealthScript> ();
		timeToDie = false;
	}

	// Update is called once per frame
	protected override void Update () {
		base.Update ();

		if (!playerGO.GetComponent<Stat_HealthScript> ().isAlive () ||
			timeToDestroy <= 0f) {

			gameObject.SetActive (false);
		}

		if ((playerCollider.bounds.Contains (new Vector2(thisCollider.bounds.min.x, thisCollider.bounds.min.y)) &&
			playerCollider.bounds.Contains (new Vector2(thisCollider.bounds.max.x, thisCollider.bounds.max.y))) && timeToDie) {

			TextPopupManager.instance.ShowTextPopup (playerGO.GetComponentInChildren<Canvas> (), playerGO.transform.position, "+"+healthScript.GetCurrentHealth (), TextPopupManager.TEXT_TYPE.HEAL);
			playerGO.GetComponent<Stat_HealthScript>().IncreaseHealth(healthScript.GetCurrentHealth());
			gameObject.SetActive(false);
		}
	}
}
