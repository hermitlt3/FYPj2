using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockActivateScript : InteractiveLock {

	private List<Vector3> resetPositions;
	private bool activate;
	// A little delay for our dearest player
	private float delayTimer = 3f;

	// Use this for initialization
	protected override void Start () {
		resetPositions = new List<Vector3> ();

		foreach (Transform trans in transform) {
			resetPositions.Add (trans.position);
		}
		activate = false;
	}
	
	// Update is called once per frame
	protected override void Update () {
		if (isTriggered) {
			delayTimer = Mathf.Max (0, delayTimer - Time.deltaTime);

			if (activate) {
				int index = 0;
				foreach (Transform trans in transform) {
					trans.position = resetPositions [index++];
					trans.gameObject.SetActive (true);
				}
				activate = false;
			}
		}

		if (delayTimer <= 0f) {
			isTriggered = false;
			isDone = true;
		}
	}

	public override void InitVariables () {
		isTriggered = true;
		activate = true;
		delayTimer = 3f;
	}
}
