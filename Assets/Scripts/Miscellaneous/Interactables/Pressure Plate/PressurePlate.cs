using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : InteractiveKey {

	public Vector3 moveDisplacement;
	public LayerMask layerMask;

	private Vector3[] wayPoints;
	private int currentWayPoint = 0;
	private bool canTrigger;

	private const float pressSpeedMultiplier = 0.5f;
	// Use this for initialization
	protected override void Start () {
		base.Start ();
		wayPoints = new Vector3[2];
		wayPoints [0] = transform.position;
		wayPoints [1] = transform.position + moveDisplacement;
		canTrigger = true;
	}
	
	// Update is called once per frame
	protected override void Update () {
		// If player is off the plate, return to original position
		if (!isTriggered && canTrigger) {
			
			Vector3 direction = (wayPoints [0] - transform.position).normalized;
			// Since it is already set the currentWayPoint is at the original position, there's no need to add to the index
			// So we can juz hardcode that the original position is at index 0
			if ((transform.position - wayPoints [0]).sqrMagnitude < 0.001f) {
				transform.position = wayPoints [0];
			} else {
				// Keep moving
				transform.position += direction * pressSpeedMultiplier * Time.deltaTime;
			}
		}
		// Player steps on the pressure plate 
		if (isTriggered && !isDone) {
			// Get direction
			Vector3 direction = (wayPoints [currentWayPoint] - transform.position).normalized;
			// If reach the desired position
			if ((transform.position - wayPoints [currentWayPoint % wayPoints.Length]).sqrMagnitude < 0.001f) {
				// Set it to the desired position in case of tiny offsets
				transform.position = wayPoints [currentWayPoint];
				// Increment waypointIndex
				currentWayPoint = (currentWayPoint + 1) % wayPoints.Length;
				// Is Done
				isDone = true;
				isTriggered = false;
				// Unleash the Kraken
				toUnlock.InitVariables();
			} else {
				// Keep moving
				transform.position += direction * pressSpeedMultiplier * Time.deltaTime;
			}
		}
		// If trap supposed to happen only once
		if (happensOnce) {
			if (isDone && toUnlock.isDone) {
				Destroy (this);
				Destroy (toUnlock.GetComponent<InteractiveLock> ());
			}
		}
		// If the trap has been unleashed
		if (toUnlock.isDone) {
			isDone = false;
			toUnlock.isDone = false;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (((1 << other.gameObject.layer) & layerMask) != 0) {
			if (!toUnlock.isTriggered && canTrigger) {
				isTriggered = true;
				canTrigger = false;
				currentWayPoint = (currentWayPoint + 1) % wayPoints.Length;
			}
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (((1 << other.gameObject.layer) & layerMask) != 0) {
			canTrigger = true;
		}
	}
}
