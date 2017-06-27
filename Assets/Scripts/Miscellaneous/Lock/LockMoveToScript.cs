using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockMoveToScript : InteractiveLock {

	public enum INPUT_TYPE
	{
		DESTINATION = 0,
		DISTANCE_TRAVEL
	}

	public INPUT_TYPE moveType;

	public Vector3 destination;
	public Vector3 moveDistance;

	public float speed = 1f;

	public bool changeDirection;

	private Vector3[] wayPoints;
	private int wayPointIndex = 0;

	// Use this for initialization
	protected override void Start () {
		wayPoints = new Vector3[2];

		switch (moveType) {
		case INPUT_TYPE.DESTINATION: 
			wayPoints [0] = transform.position;
			wayPoints [1] = destination;
			break;

		case INPUT_TYPE.DISTANCE_TRAVEL:
			wayPoints [0] = transform.position;
			wayPoints [1] = transform.position + moveDistance;
			break;
		}
	}
	
	// Update is called once per frame
	protected override void Update () {
		if (changeDirection) {
			wayPointIndex = (wayPointIndex + 1) % wayPoints.Length;
			changeDirection = false;
		}

		if (isTriggered) {
			
			Vector3 direction = (wayPoints [wayPointIndex] - transform.position).normalized;
			if ((wayPoints [wayPointIndex] - transform.position).sqrMagnitude < 0.1f) {
				isTriggered = false;
				isDone = true;
			} else {
				transform.position += direction * speed * Time.deltaTime;
			}
		}
	}

	public override void InitVariables ()
	{
		isTriggered = true;
		changeDirection = true;
	}
}
