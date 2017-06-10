using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* @brief An encapsulated class of key and lock to store in puzzle manager */
public class InteractiveObject : MonoBehaviour {

	[SerializeField]
	private InteractiveKey theTrigger;
	[SerializeField]
	private InteractiveLock theResults;

	private bool happensOnce;

	// Use this for initialization
	void Start () {
		PuzzleManager.instance.AddToPuzzleList (this);
		happensOnce = theTrigger.DoesItHappenOnce ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Reset() {
		theTrigger.Reset ();
		theResults.Reset ();
	}

	public InteractiveKey GetKey() {
		return theTrigger;
	}

	public InteractiveLock GetLock() {
		return theResults;
	}
}
