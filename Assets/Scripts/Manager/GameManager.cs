﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Where everything works
public class GameManager : MonoBehaviour {
   
    public static GameManager instance;
	public GameObject player;

	void Awake() {
        if (instance && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this);
    }

	// Use this for initialization
	void Start () {
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
        GameObject bossArena = GameObject.FindGameObjectWithTag("Boss Arenas");
        if (bossArena)
        {
            bossArena.GetComponent<BossArenaScript>().Reset();
        }

		// Trace. On.
	}

    public void LoadPlayer()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

	public bool GoNextLevel(string sceneName) {

        StartCoroutine(SceneTransitManager.instance.ChangeScene(sceneName));
        return false;
	}

    public void BackToMainMenu()
    {
        SceneTransitManager.instance.AddGameObjectToDestroy(Camera.main.gameObject);
        SceneTransitManager.instance.AddGameObjectToDestroy(player);
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Canvas");
        foreach (GameObject go in gos)
        {
            SceneTransitManager.instance.AddGameObjectToDestroy(go);
        }
        StartCoroutine(SceneTransitManager.instance.ChangeScene("Main Menu"));
    }

	void CursorTextureChange() {
        Texture2D cursorClicked = Resources.Load("Cursors/CursorClick") as Texture2D;
        Texture2D cursor = Resources.Load("Cursors/Cursor") as Texture2D;

        if (Input.GetButton("Fire1")) {
			Cursor.SetCursor (cursorClicked, new Vector2(cursorClicked.width, 0), CursorMode.Auto);
		} else {
			Cursor.SetCursor (cursor, new Vector2(cursorClicked.width, 0), CursorMode.Auto);
		}
	}
}
