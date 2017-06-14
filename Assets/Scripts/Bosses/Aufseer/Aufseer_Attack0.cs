using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aufseer_Attack0 : Boss_Attack {

	private int attackPatternIndex = 0;
	private Aufseer_Pool pool;

	// Use this for initialization
	void Start () {
		// In case the target isnt set properly, then we manually do tis
		if (target == null) {
			target = GameObject.FindGameObjectWithTag ("Player");
		}
		pool = GetComponent<Aufseer_Pool> ();
		doAction ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void doAction() {
		for (int i = 0; i < 10; ++i) {
			GameObject temp = pool.GetPooledObject (attackPatternIndex);
			temp.SetActive (true);
			temp.transform.parent = transform;
			temp.transform.position = transform.position;
		}
		Destroy (this);
	}
}
