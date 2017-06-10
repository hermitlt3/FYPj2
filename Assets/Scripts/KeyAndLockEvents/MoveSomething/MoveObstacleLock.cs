using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstacleLock : InteractiveLock {

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
		resetPosition = transform.position;
		stopPosition = resetPosition + moveToOffsetFromPosition;
		towards = true;
	}

	// Update is called once per frame
	protected override void Update () {
		base.Update ();
		if (isTriggered) {
			if (towards && (transform.position - stopPosition).magnitude > 0.1f) {
				transform.position += (stopPosition - transform.position).normalized * moveSpeed * Time.deltaTime;
			} else if (towards && (transform.position - stopPosition).magnitude < 0.1f) {
				towards = false;
				isTriggered = false;

				if (happensOnce) {
					Destroy (this);
				}
			}
				
			if (!towards && (transform.position - resetPosition).magnitude > 0.1f) {
				transform.position += (resetPosition - transform.position).normalized * moveSpeed * Time.deltaTime;
			} else if (!towards && (transform.position - resetPosition).magnitude < 0.1f) {
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
