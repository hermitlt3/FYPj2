﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextPopupScript : MonoBehaviour {

	private Text outputText;
	private Outline textOutline;

	void Awake () {

		outputText = GetComponent<Text> ();
		textOutline = GetComponent<Outline> ();

	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
	// Called when the animation for the popup ends
	void DeleteItself() {
		Destroy (this.gameObject);
	}

	public void SetText(string text, Color color) {
		outputText.text = text;
		outputText.color = color;
	}

	public void SetOutline(Color color) {
		textOutline.effectColor = color;
	}
}
