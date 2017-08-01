using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArenaScript : MonoBehaviour {

	GameObject player;
	BoxCollider2D bossArea;
	GameObject mainCamera;

	public GameObject[] blockingGO;
	public GameObject boss;
	public GameObject checkPointUnlocked;

	public float xOffset = 5f;

	public bool DestroyArea;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		mainCamera = GameObject.FindGameObjectWithTag ("MainCamera");
		bossArea = GetComponent<BoxCollider2D> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (boss.GetComponent<Boss_AI>().shouldDie) {
			foreach (GameObject go in blockingGO) {
				go.SetActive (false);
			}

			mainCamera.GetComponent<BossCamera> ().ResetToFollow ();
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
            if (boss.GetComponent<Boss_AI>().shouldReset)
            {
                boss.GetComponent<Stat_HealthScript>().SetCurrentHealth(boss.GetComponent<Stat_HealthScript>().GetMaxHealth());
            }
            boss.transform.GetChild(0).gameObject.SetActive(true);

            mainCamera.GetComponent<Camera2DFollow> ().enabled = false;
			mainCamera.GetComponent<BossCamera> ().enabled = true;
			mainCamera.GetComponent<BossCamera> ().moveToPosition = bossArea.bounds.center;
			mainCamera.GetComponent<BossCamera> ().SetOrthoTargetSize (bossArea.bounds.size.x + xOffset * 2);
		}
	}

	public void Reset() {
		foreach (GameObject go in blockingGO) {
			go.SetActive (false);
		}
        boss.GetComponent<Boss_AI>().Reset ();
		//boss.SetActive (false);
	}
}
