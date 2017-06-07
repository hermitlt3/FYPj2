using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_Die : MonoBehaviour {

	[SerializeField]
	private bool youOnlyLiveOnce = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Deactivate() {
		CollectiblesGenerator.instance.GenerateCollectibles (transform.position, 10);
		if (youOnlyLiveOnce) {
			ReloadCheckpointSystem.RemoveEnemyToReloadList (this.gameObject);

		} else {
			this.gameObject.SetActive (false);
		}
	}
}
