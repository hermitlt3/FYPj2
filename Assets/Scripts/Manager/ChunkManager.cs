using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour {

	public float chunkSizeX;
	public float offSetX;
	private GameObject player;
	private BoxCollider2D myBoundary;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");	
		//transform.position = player.transform.position;
		myBoundary = GetComponent<BoxCollider2D> ();
		myBoundary.size = new Vector2 (chunkSizeX + offSetX, 1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
