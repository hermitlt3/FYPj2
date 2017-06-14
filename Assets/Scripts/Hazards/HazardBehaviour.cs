using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardBehaviour : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Player") {
			float damage = coll.GetComponent<Stat_HealthScript> ().GetMaxHealth ();
			coll.GetComponent<Stat_HealthScript> ().DecreaseHealth (damage);
		}
	}
}
