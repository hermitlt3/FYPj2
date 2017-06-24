using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverPuzzleKey : InteractiveKey {

	public float rotateZ;

	protected override void Start () {
		base.Start ();
	}

	// Update is called once per frame
	protected override void Update () {
		base.Update ();

		if (isTriggered) {
			transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotateZ));
			isTriggered = false;
			isDone = true;
			toUnlock.isTriggered = true;
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
