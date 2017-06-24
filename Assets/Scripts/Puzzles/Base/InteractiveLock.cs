using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveLock : MonoBehaviour {

	// Logic is this. When the key is activated , lock will unlock the event

	public bool isTriggered;			// In case whereby the key can switch on and off and health is not recommended
	public bool isDone;

	// Use this for initialization
	protected virtual void Start () {
		isTriggered = isDone = false;
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		// Do the update of the keyTransform in derived class
	}

	public virtual void Reset() {
		isTriggered = false;
	}
}
