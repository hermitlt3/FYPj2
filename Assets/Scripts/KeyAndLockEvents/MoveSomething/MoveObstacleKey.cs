using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstacleKey : InteractiveKey {

	private Vector3 resetPosition;

	[SerializeField]
	private Vector3 moveToOffsetFromPosition;
	private Vector3 stopPosition;

	[SerializeField]
	private float moveSpeed = 3f;
	private bool towards;	// true towards, false away
	//private bool 
	// Use this for initialization
	protected override void Start () {
		base.Start ();

		resetPosition = transform.localPosition;
		stopPosition = resetPosition + moveToOffsetFromPosition;
		towards = true;
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();

		if (isTriggered) {
			if (towards && (transform.localPosition - stopPosition).magnitude > 0.1f) {
				transform.position += (stopPosition - transform.localPosition).normalized * moveSpeed * Time.deltaTime;
				if (healthScript.GetCurrentHealth () != 1) {
					healthScript.IncreaseHealth (1 - healthScript.GetCurrentHealth ());
				}
			} else if (towards && (transform.localPosition - stopPosition).magnitude < 0.1f) {
				PuzzleManager.instance.UpdateKey (this);
				towards = false;
				isTriggered = false;

				if (happensOnce) {
					Destroy (this);
				}
			}


			if (!towards && (transform.localPosition - resetPosition).magnitude > 0.1f) {
				transform.position += (resetPosition - transform.localPosition).normalized * moveSpeed * Time.deltaTime;
				if (healthScript.GetCurrentHealth () != 1) {
					healthScript.IncreaseHealth (1 - healthScript.GetCurrentHealth ());
				}
			} else if (!towards && (transform.localPosition - resetPosition).magnitude < 0.1f) {
				PuzzleManager.instance.UpdateKey (this);
				towards = true; 
				isTriggered = false;

				if (happensOnce) {
					Destroy (this);
				}
			}
		}
	}

	public override void Reset() {
		base.Reset ();
		transform.position = resetPosition;
	}
}
