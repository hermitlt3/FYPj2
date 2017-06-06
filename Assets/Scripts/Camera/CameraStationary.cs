using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStationary : MonoBehaviour {

	public Transform target;
	public Vector3 moveToPosition;

	public float damping = 1;
	public float sizeIncrease;
	public float xOffset;

	private Vector3 velocity;
	private float size;
	private float targetSize;
	// Use this for initialization
	void Start () {
		size = GetComponent<Camera> ().orthographicSize;
		targetSize = sizeIncrease + size;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 newPos = Vector3.SmoothDamp(transform.position, moveToPosition  + new Vector3(xOffset, 0), ref velocity, damping);
		GetComponent<Camera> ().orthographicSize = Mathf.Min (GetComponent<Camera> ().orthographicSize + Time.deltaTime * 3f, targetSize);
		transform.position = newPos;
	}

	public void ResetToFollow() {
		GetComponent<Camera2DFollow> ().enabled = true;
		GetComponent<CameraStationary> ().enabled = false;
	}
}
