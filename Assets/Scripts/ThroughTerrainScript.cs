using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThroughTerrainScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
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
			transform.GetComponent<SpriteRenderer>().sortingLayerName = "In front player";
		}
	}

	public void MoveSpriteToBack() {
		foreach (Transform transform in transform) {
			transform.GetComponent<SpriteRenderer>().sortingLayerName = "Behind player";
		}
	}
}
