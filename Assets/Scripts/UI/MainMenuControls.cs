using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenuControls : MonoBehaviour {

    public EventSystem System;

	// Use this for initialization
	void Start () {
        gameObject.name = "Continue Button";
        System.firstSelectedGameObject = gameObject;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
