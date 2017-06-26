using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour {

	public LayerMask killLayers;
	Collider2D killZone;

	// Use this for initialization
	void Start () {
		killZone = GetComponent<Collider2D> ();
		killZone.isTrigger = true;

		if (!GetComponent<Rigidbody2D> ()) {
			this.gameObject.AddComponent<Rigidbody2D> ().isKinematic = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (((1 << coll.gameObject.layer) & killLayers) != 0) {

			// Destroy health
			if (coll.GetComponent<Stat_HealthScript> ()) {
				int damage = coll.GetComponent<Stat_HealthScript> ().GetMaxHealth ();
				coll.GetComponent<Stat_HealthScript> ().DecreaseHealth (damage);
			}
		}
	}
}
