using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderScript : MonoBehaviour {

	public bool isDestroying = true;
	public GameObject toBeDestroyed;

	public float dangerSpeed = 2f;

	// Use this for initialization
	void Start () {
			
	}
	
	// Update is called once per frame
	void Update () {
		if (GetComponent<Rigidbody2D> ().velocity.sqrMagnitude > dangerSpeed) {
			// Change to collectible becuz it has the same behavior as collectible
			this.gameObject.layer = LayerMask.NameToLayer ("Collectible");
		} else {
			this.gameObject.layer = LayerMask.NameToLayer ("Terrain");
		}
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (isDestroying && other.gameObject == toBeDestroyed) {
			Destroy (this.gameObject);
			Destroy (other.gameObject);
		}
	}
}
