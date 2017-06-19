﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblesGenerator : MonoBehaviour {

	public static CollectiblesGenerator instance;

//	[SerializeField]
//	private float healthChance = 0.2f;

	// The number of game objects instantiated = exact value / divider
	[SerializeField]
	private float numberOfExpDivider = 2f;

	// Spawn velocity of the collectibles
	[SerializeField]
	private Vector2 velocityMagnitude = new Vector2 (10, 100);

	// In case we want do a stat that increases the drop rate of the collectibles
	// Therefore a reference a player here
//	private GameObject player;

	void Awake() {
		if (instance && instance != this) {
			Destroy (instance);
			return;
		}

		instance = this;
	}

	// Use this for initialization
	void Start () {
//		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GenerateCollectibles(Vector3 position, float expValue) {
		int healthAmount = 0;//(healthChance <= Random.Range (0f, 1f)) ? 5:0;
		int numOfExp = Mathf.CeilToInt (expValue / numberOfExpDivider);
		int totalSprites = healthAmount + numOfExp;
			
		for (int i = Mathf.FloorToInt (-totalSprites / 2); i <= Mathf.FloorToInt (totalSprites / 2); ++i) {
			print (i);
			if (healthAmount > 0 && i % (totalSprites / healthAmount) == 0) {


			} else {
				GameObject collectible = CollectiblePoolScript.instance.GetPooledObject ();
				collectible.SetActive (true);
				collectible.transform.position = position;
				collectible.GetComponent<CollectibleBehavior> ().Reset ();

				collectible.GetComponent<Rigidbody2D> ().velocity = new Vector2 ((float)i/(float)totalSprites * velocityMagnitude.x, velocityMagnitude.y);
				collectible.GetComponent<Stat_ExperienceScript> ().SetExperience (expValue / numOfExp);
			}

		}
	}

	public void DeactivateAll() {
		CollectiblePoolScript.instance.DeactivateAll ();
	}
}
