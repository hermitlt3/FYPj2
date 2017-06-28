using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAreaScript : MonoBehaviour {

	void Start () {

	}

	void OnTriggerExit2D(Collider2D colli) 
	{
		if (colli.gameObject.CompareTag("Player")) {
			colli.transform.gameObject.GetComponent<Stat_HealthScript> ().DecreaseHealth (colli.gameObject.GetComponent<Stat_HealthScript> ().GetCurrentHealth ());
		}
	}
}
