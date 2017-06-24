using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverPuzzleKey : InteractiveKey {

	public float rotateZ;
	private bool reverse;
	protected override void Start () {
		base.Start ();
		reverse = false;
	}

	// Update is called once per frame
	protected override void Update () {
		base.Update ();
		if (isTriggered) {
			if (!reverse) {
				transform.rotation = Quaternion.Euler (new Vector3 (0, 0, rotateZ));
				reverse = true;
			} else {
				transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 0));
				reverse = false;
			}
			isDone = true;
			toUnlock.InitVariables ();
			Reset ();
		}
		if (isDone) {
			healthScript.SetCurrentHealth (1);
		}
		if (toUnlock.isDone) {
			isDone = false;
			toUnlock.isDone = false;
		}
	}

	public override void Reset() {
		base.Reset ();
	}
}
