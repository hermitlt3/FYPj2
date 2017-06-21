using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Where everything works
public class GameManager : MonoBehaviour {

	public static GameManager instance;
	private GameObject player;

	void Awake() {
		DontDestroyOnLoad (this.gameObject);

		if (instance && instance != this) {
			Destroy (instance);
			return;
		}
		instance = this;
	}

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");

	}
	
	// Update is called once per frame
	void Update () {

	}

	public void OnPlayerDead() {

		// Fade. On.
		// Managers. On.
		ReloadCheckpointSystem.ReloadAll ();
		CollectiblesGenerator.instance.DeactivateAll ();

		// Camera. On.
		Camera.main.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, Camera.main.transform.position.z);
		Camera.main.GetComponent<Camera2DFollow> ().enabled = true;

		// Player. On.
		player.GetComponent<Stat_HealthScript>().IncreaseHealth (player.GetComponent<Stat_HealthScript>().GetMaxHealth ());
		player.GetComponent<Animator>().SetBool ("Dead", false);
		player.transform.position = player.GetComponent<Player_Spawnpoint> ().GetSpawnLocation ();
		player.GetComponent<Player_Input> ().enabled = true;
	
		// Boss. On.
		GameObject.FindGameObjectWithTag("Boss Arenas").GetComponent<BossArenaScript>().Reset();

		// Trace. On.
	}

	public bool LoadScene() {
		return false;
	}
}
