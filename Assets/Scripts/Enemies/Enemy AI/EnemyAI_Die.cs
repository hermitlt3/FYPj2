using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_Die : MonoBehaviour {

	[SerializeField]
	private bool youOnlyLiveOnce = false;

	private bool shouldDie = true;

	private Stat_ExperienceScript expOutputScript;
	// Use this for initialization
	void Start () {
		expOutputScript = GetComponent<Stat_ExperienceScript> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Deactivate() {
		if (!shouldDie) {
			return;
		}

		CollectiblesGenerator.instance.GenerateCollectibles (transform.position, expOutputScript.GetExperience());
		if (youOnlyLiveOnce) {
			ReloadCheckpointSystem.RemoveEnemyToReloadList (this.gameObject);

		} else {
			this.gameObject.SetActive (false);
		}
	}

	void ShouldDie() {
		if (!GameObject.FindGameObjectWithTag ("Player").GetComponent<Stat_HealthScript> ().isAlive ()) {
			shouldDie = false;
		}
	}

	public void Reset() {
		shouldDie = true;
	}
}
