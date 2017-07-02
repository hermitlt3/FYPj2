﻿using System.Collections;
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
		CursorTextureChange();
	}

	public void OnPlayerDead() {

		// Fade. On.
		// Managers. On.
		ReloadCheckpointSystem.ReloadAll ();
		CollectiblesGenerator.instance.DeactivateAll ();

		// Player. On.
		player.GetComponent<Stat_HealthScript>().IncreaseHealth (player.GetComponent<Stat_HealthScript>().GetMaxHealth ());
		player.GetComponent<Player_Experience> ().LoseExperience ();
		player.GetComponent<Animator>().SetBool ("Dead", false);
		player.transform.position = player.GetComponent<Player_Spawnpoint> ().GetSpawnLocation ();
		player.GetComponent<Player_Input> ().enabled = true;

		// Camera. On.
		Camera.main.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, Camera.main.transform.position.z);
		Camera.main.GetComponent<Camera2DFollow> ().enabled = true;

		// Boss. On.
		GameObject.FindGameObjectWithTag("Boss Arenas").GetComponent<BossArenaScript>().Reset();

		// Trace. On.
	}

	public bool LoadScene() {
		return false;
	}

	void CursorTextureChange() {
		if (Input.GetButton("Fire1")) {
			Cursor.SetCursor ((Resources.Load ("Cursors/CursorClick")) as Texture2D, new Vector2(82, 41), CursorMode.Auto);
		} else {
			Cursor.SetCursor ((Resources.Load ("Cursors/Cursor")) as Texture2D, new Vector2(82, 41), CursorMode.Auto);
		}
	}
}
