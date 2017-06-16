using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillToggleScript : MonoBehaviour {

	private Toggle myToggle;

	// Use this for initialization
	void Start () {
		myToggle = GetComponent<Toggle> ();
		myToggle.group = transform.parent.parent.parent.GetComponentInChildren<ToggleGroup> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
