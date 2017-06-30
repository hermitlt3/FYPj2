using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour {

	protected GameObject player;
	protected Image barImage;

	// Use this for initialization
	protected virtual void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");	
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		
	}
}
