using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThroughTerrainScript : MonoBehaviour {

	public bool stayFront;
	public bool stayBack;
	// Use this for initialization
	void Start () {
		if (stayFront) {
			stayBack = false;
		} 
		if (stayBack) {
			stayFront = false;
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
		

	void OnTriggerExit2D(Collider2D collision) {
		if (collision.transform.gameObject.tag == "Player") {
			MoveSpriteToBack ();		}
	}

	public void MoveSpriteToFront() {
		foreach (Transform transform in transform) {
			if (!stayBack || stayBack && transform.GetComponent<ThroughTerrainScript> () && transform != this.transform) {
				transform.GetComponent<SpriteRenderer> ().sortingLayerName = "In front player";
			}
		}
	}

	public void MoveSpriteToBack() {
		foreach (Transform transform in transform) {
			if (!stayFront || stayFront && transform.GetComponent<ThroughTerrainScript> () && transform != this.transform) {
				transform.GetComponent<SpriteRenderer> ().sortingLayerName = "Behind player";
			}
		}
	}
}
