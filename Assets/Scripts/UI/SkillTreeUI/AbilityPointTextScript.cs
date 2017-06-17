using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityPointTextScript : MonoBehaviour {

	private Stat_AbilityPoint playerAP;
	private Text myText;

	// Use this for initialization
	void Start () {
		playerAP = GameObject.FindGameObjectWithTag("Player").GetComponent<Stat_AbilityPoint> ();
		myText = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		myText.text = playerAP.GetAbilityPoint().ToString ();
	}
}
