using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2DResolutions : MonoBehaviour {

	public float targetAspectRatioX = 16f;
	public float targetAspectRatioY = 9f;

	// Use this for initialization
	void Start () {

		// Set the desired ratio
		float targetaspect = targetAspectRatioX / targetAspectRatioY;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
