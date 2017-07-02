using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCamera : MonoBehaviour {

	public Vector3 moveToPosition;

	public float damping = 1;

	private Vector3 velocity;
	private float size;
	private float targetSize;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.SmoothDamp(transform.position, moveToPosition, ref velocity, damping);

		GetComponent<Camera> ().orthographicSize = Mathf.Min (GetComponent<Camera> ().orthographicSize + Time.deltaTime * 3f, targetSize);
	}

	public void ResetToFollow() {
		GetComponent<Camera2DFollow> ().enabled = true;
		GetComponent<BossCamera> ().enabled = false;
	}

	public void SetOrthoTargetSize(float widthToBeSeen) {
		targetSize = widthToBeSeen * Screen.height / Screen.width * 0.5f;
	}
}
