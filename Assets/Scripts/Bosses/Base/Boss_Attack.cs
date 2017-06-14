using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Attack : MonoBehaviour {

	protected GameObject target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public virtual void doAction() {

	}

	public bool SetTarget(GameObject ob) {
		target = ob;
		return true;
	}
}
