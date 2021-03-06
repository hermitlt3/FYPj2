﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorToNextSceneScript : MonoBehaviour {

    public Boss_AI theBoss;
    public string nextLevelName = "Level 2";
    Animator animator;
    bool startAnim;
    AudioSource sound;
    bool soundPlayed;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        sound = GetComponent<AudioSource>();
        startAnim = false;
        soundPlayed = false;

    }
	
	// Update is called once per frame
	void Update () {
		if(!startAnim && theBoss.shouldDie)
        {
            startAnim = true;
            animator.SetBool("Play", startAnim);
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if (sound && !soundPlayed)
            {
                sound.Play();
                soundPlayed = true;
            }
            GameManager.instance.player.GetComponent<Player_Input>().enabled = false;
            ReloadCheckpointSystem.DestroyStageEnemies();
            StartCoroutine(SceneTransitManager.instance.ChangeScene(nextLevelName));
        }
    }
}
