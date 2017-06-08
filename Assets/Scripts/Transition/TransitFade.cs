﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitFade : MonoBehaviour {

	public Texture2D fadedTexture;		// Texture that will overlay the screen
	public float fadeSpeed = 0.8f;		// The fading speed

	private int drawDepth = -1000;		// The texture order in the draw hierachy; low number renders on top
	private float alpha = 1f;			// Alpha of the texture between 0 and 1
	private int fadeDir = -1;			// Fade in = 1, fade out = -1

	void OnGUI() {
		alpha += fadeDir * fadeSpeed * Time.deltaTime;
		alpha = Mathf.Clamp01 (alpha);

		GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha);
		GUI.depth = drawDepth;
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), fadedTexture);
	}

	public float BeginFade(int direction) {
		fadeDir = direction;
		return fadeSpeed;
	}
}
