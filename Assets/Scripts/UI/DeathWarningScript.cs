using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathWarningScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Camera.main.WorldToScreenPoint (new Vector2(Screen.width / 2 , Screen.height / 2));
	}
}
