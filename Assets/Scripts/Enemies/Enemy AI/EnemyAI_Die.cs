using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_Die : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Deactivate() {
		this.gameObject.SetActive (false);
	}
}
