using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlaySoundOnClickScript : MonoBehaviour, IPointerDownHandler{

    public AudioSource onCickSound;

    // Use this for initialization
    void Start () {
		if(onCickSound == null)
        {
            print("No sound on click");
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerDown(PointerEventData eventData)
    {
        onCickSound.Play();
    }
}
