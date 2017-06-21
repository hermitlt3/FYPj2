using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Make sure health = 1
[RequireComponent(typeof(Stat_HealthScript))]

public class InteractiveKey : MonoBehaviour {

	// Logic is this. We are going to assume the thing to interact has a fake "health" of 1. 
	// If player attacks it and kills it, it triggers the lock it is attached to
	protected Stat_HealthScript healthScript;

	[SerializeField]
	protected bool isTriggered;			// In case whereby the key can switch on and off and health is not recommended

	[SerializeField]
	protected bool happensOnce;

	// Use this for initialization
	protected virtual void Start () {
		healthScript = GetComponent<Stat_HealthScript> ();
		isTriggered = false;
	}
	
	// Update is called once per frame
	protected virtual void Update () {

		if(transform.parent.GetComponentInChildren<InteractiveLock> ().IsTriggered())
			healthScript.IncreaseHealth (1 - healthScript.GetCurrentHealth ());
		
		if (healthScript.isAlive () == false && !transform.parent.GetComponentInChildren<InteractiveLock> ().IsTriggered()) {
			isTriggered = true;
		}

		// PuzzleManager.instance.UpdateKey (this);
		// I did not call updatekey here in case u wan a delay whereby the key is done first before the lock is unlocked

	}

	public virtual void Reset() {
		isTriggered = false;
		if (healthScript.GetCurrentHealth () != 1) {
			healthScript.IncreaseHealth (1 - healthScript.GetCurrentHealth ());
		}
	}


	public bool DoesItHappenOnce() {
		return happensOnce;
	}

	public void SetTrigger(bool value) {
		isTriggered = value;
	}
}
