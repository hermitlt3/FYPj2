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
		
	void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.CompareTag("Player")) {
			Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other.collider);
		}
	}

	void OnTriggerExit2D(Collider2D collision) {
		if (collision.gameObject.CompareTag("Player")) {
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
