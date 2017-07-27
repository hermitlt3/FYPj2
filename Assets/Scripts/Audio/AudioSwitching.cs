using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSwitching : MonoBehaviour {

    public GameObject boss;
    AudioSource[] audio;

	// Use this for initialization
	void Start () {
        audio = GetComponents<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (boss.activeInHierarchy && boss.GetComponent<Stat_HealthScript>().isAlive())
        {
            audio[1].enabled = true;
            audio[0].enabled = false;
        }
        else
        {
            audio[1].enabled = false;
            audio[0].enabled = true;
        }
	}
}
