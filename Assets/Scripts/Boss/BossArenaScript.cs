using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArenaScript : MonoBehaviour {

	GameObject player;
	BoxCollider2D areaCollide;
	GameObject mainCamera;

	public GameObject blockingGO;
	public GameObject boss;
	public GameObject checkPointUnlocked;

	public bool DestroyArea;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		mainCamera = GameObject.FindGameObjectWithTag ("MainCamera");
		areaCollide = GetComponent<BoxCollider2D> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (!boss.activeInHierarchy) {
			blockingGO.SetActive (false);
			mainCamera.GetComponent<CameraStationary> ().ResetToFollow ();
			checkPointUnlocked.SetActive (true);
			if(DestroyArea)
				Destroy (this.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D collision) {
		if (blockingGO.activeInHierarchy)
			return;
		
		if (collision.gameObject == player && boss.activeInHierarchy) {
			blockingGO.SetActive (true);
			mainCamera.GetComponent<Camera2DFollow> ().enabled = false;
			mainCamera.GetComponent<CameraStationary> ().enabled = true;
			mainCamera.GetComponent<CameraStationary> ().moveToPosition = transform.position;
		}
	}
}
