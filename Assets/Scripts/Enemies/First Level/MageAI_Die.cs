using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageAI_Die : EnemyAI_Die {

	// Use this for initialization
	void Start () {
		expOutputScript = GetComponent<Stat_ExperienceScript> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	protected override void Deactivate() {

		base.Deactivate ();

		CollectiblesGenerator.instance.GenerateCollectibles (transform.position, expOutputScript.GetExperience(), Random.Range(5, 20));
		if (youOnlyLiveOnce) {
			ReloadCheckpointSystem.RemoveEnemyToReloadList (this.gameObject);

		} else {
			this.gameObject.SetActive (false);
		}
	}

	protected override void ShouldDie() {
		if (!GameObject.FindGameObjectWithTag ("Player").GetComponent<Stat_HealthScript> ().isAlive ()) {
			shouldDie = false;
		}
	}
}
