using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArenaScript : MonoBehaviour {

	GameObject player;
	//BoxCollider2D areaCollide;
	GameObject mainCamera;

	public GameObject[] blockingGO;
	public GameObject boss;
	public GameObject checkPointUnlocked;

	public bool DestroyArea;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		mainCamera = GameObject.FindGameObjectWithTag ("MainCamera");
		//areaCollide = GetComponent<BoxCollider2D> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (!boss.GetComponent<Stat_HealthScript>().isAlive()) {
			foreach (GameObject go in blockingGO) {
				go.SetActive (false);
			}

			mainCamera.GetComponent<CameraStationary> ().ResetToFollow ();
			checkPointUnlocked.SetActive (true);

			if(DestroyArea)
				Destroy (this.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D collision) {
		foreach (GameObject go in blockingGO) {
			if (go.activeInHierarchy)
				return;
		}

		
		if (collision.gameObject == player && boss.GetComponent<Stat_HealthScript>().isAlive()) {
			foreach (GameObject go in blockingGO) {
				go.SetActive (true);
			}
			boss.SetActive (true);

			mainCamera.GetComponent<Camera2DFollow> ().enabled = false;
			mainCamera.GetComponent<CameraStationary> ().enabled = true;
			mainCamera.GetComponent<CameraStationary> ().moveToPosition = transform.position;
		}
	}

	public void Reset() {
		foreach (GameObject go in blockingGO) {
			go.SetActive (false);
		} 
		foreach (Boss_AI ai in boss.GetComponents<Boss_AI>()) {
			ai.Reset ();
		}

		boss.SetActive (false);
	}
}
