using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSwitching : MonoBehaviour {

    public GameObject boss;
    AudioSource[] audios;
    public bool lastStage;

	// Use this for initialization
	void Start () {
        audios = GetComponents<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if(!lastStage)
        {
            if (boss.activeInHierarchy && boss.GetComponent<Stat_HealthScript>().isAlive())
            {
                audios[1].enabled = true;
                audios[0].enabled = false;
            }
            else
            {
                audios[1].enabled = false;
                audios[0].enabled = true;
            }
        }
        else
        {
            if (!boss.GetComponent<Stat_HealthScript>().isAlive())
            {
                audios[1].enabled = true;
                audios[0].enabled = false;
            }
        }
	}
}
