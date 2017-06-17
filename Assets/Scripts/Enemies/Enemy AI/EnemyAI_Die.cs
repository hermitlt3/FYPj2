using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_Die : MonoBehaviour {

	[SerializeField]
	private bool youOnlyLiveOnce = false;
	private Stat_ExperienceScript expOutputScript;
	// Use this for initialization
	void Start () {
		expOutputScript = GetComponent<Stat_ExperienceScript> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Deactivate() {
		CollectiblesGenerator.instance.GenerateCollectibles (transform.position, expOutputScript.GetExperience());
		if (youOnlyLiveOnce) {
			ReloadCheckpointSystem.RemoveEnemyToReloadList (this.gameObject);

		} else {
			this.gameObject.SetActive (false);
		}
	}
}
