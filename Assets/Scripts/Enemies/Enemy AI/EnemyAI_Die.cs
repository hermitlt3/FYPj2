using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_Die : MonoBehaviour {

	[SerializeField]
	private bool youOnlyLiveOnce;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Deactivate() {
		if (youOnlyLiveOnce) {
			ReloadCheckpointSystem.RemoveEnemyToReloadList (this.gameObject);
			Destroy (this.gameObject);
		} else {
			this.gameObject.SetActive (false);
		}
	}
}
