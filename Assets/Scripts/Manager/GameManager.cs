using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Where everything works
public class GameManager : MonoBehaviour {

	public static GameManager instance;
	private GameObject player;

	void Awake() {
		DontDestroyOnLoad (this.gameObject);

		if (instance) {
			Destroy (instance);
			return;
		}
		instance = this;
	}

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");

	}
	
	// Update is called once per frame
	void Update () {

	}

	public void OnPlayerDead() {

	}
}
