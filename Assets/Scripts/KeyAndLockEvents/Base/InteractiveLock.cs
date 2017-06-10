using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveLock : MonoBehaviour {

	// Logic is this. When the key is activated , lock will unlock the event

	[SerializeField]
	protected bool isTriggered;			// In case whereby the key can switch on and off and health is not recommended

	protected bool happensOnce;			// This will be assigned in the Puzzle Manager by the key

	// Use this for initialization
	protected virtual void Start () {
		isTriggered = false;
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		// Do the update of the keyTransform in derived class
	}

	public virtual void Reset() {
		isTriggered = false;
	}

	public void SetTrigger(bool value) {
		isTriggered = value;
	}

	public bool IsTriggered() {
		return isTriggered;
	}

	public void WillItHappenOnce(bool value) {
		happensOnce = value;
	}
}
