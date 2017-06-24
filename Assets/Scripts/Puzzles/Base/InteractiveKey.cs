using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Make sure health = 1
[RequireComponent(typeof(Stat_HealthScript))]

public class InteractiveKey : MonoBehaviour {

	// Logic is this. We are going to assume the thing to interact has a fake "health" of 1. 
	// If player attacks it and kills it, it triggers the lock it is attached to
	protected Stat_HealthScript healthScript;

	public bool isTriggered;			// In case whereby the key can switch on and off and health is not recommended
	public bool happensOnce;			// Only happens once
	public bool isDone;

	public InteractiveLock toUnlock;

	// Use this for initialization
	protected virtual void Start () {
		healthScript = GetComponent<Stat_HealthScript> ();
		isTriggered = happensOnce = isDone = false;
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		if (healthScript.isAlive () == false) {
			isTriggered = true;
		}
	}

	public virtual void Reset() {
		isTriggered = false;
		healthScript.SetCurrentHealth (1);
	}
}
