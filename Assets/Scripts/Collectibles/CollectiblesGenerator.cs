using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblesGenerator : MonoBehaviour {

	public static CollectiblesGenerator instance;
//	[SerializeField]
	private float healthChance = 1f;

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

	public void GenerateCollectibles(Vector3 position, float expValue, float hpValue = 5f) {
		GenerateEXP (position, expValue);
		GenerateHealth (position, hpValue);
	}

	public void DeactivateAll() {
		CollectiblePoolScript.instance.DeactivateAll ();
	}

	public bool GenerateEXP(Vector3 position, float value) {
		int numOfExp = Mathf.CeilToInt (value / numberOfExpDivider);

		for (int i = Mathf.FloorToInt (-numOfExp / 2); i <= Mathf.FloorToInt (numOfExp / 2); ++i) {
			GameObject collectible = CollectiblePoolScript.instance.GetPooledObject (0);
			collectible.SetActive (true);
			collectible.transform.position = position;
			collectible.GetComponent<CollectibleBehavior> ().Reset ();

			collectible.GetComponent<Rigidbody2D> ().velocity = new Vector2 ((float)i / (float)numOfExp * velocityMagnitude.x, velocityMagnitude.y);
			collectible.GetComponent<Stat_ExperienceScript> ().SetExperience (value / numOfExp);
		}
		return true;
	}

	public bool GenerateHealth(Vector3 position, float value) {
		int numOfHealth = (healthChance >= Random.Range (0f, 1f)) ? Random.Range (1, 5) : 0;

		for (int i = Mathf.FloorToInt (-numOfHealth / 2); i <= Mathf.FloorToInt (numOfHealth / 2); ++i) {
			GameObject collectible = CollectiblePoolScript.instance.GetPooledObject (1);
			collectible.SetActive (true);
			collectible.transform.position = position;
			collectible.transform.position -= new Vector3 (0, 0, 0.5f);
			collectible.GetComponent<CollectibleBehavior> ().Reset ();

			collectible.GetComponent<Rigidbody2D> ().velocity = new Vector2 ((float)i / (float)numOfHealth * velocityMagnitude.x, velocityMagnitude.y);
			collectible.GetComponent<Stat_HealthScript> ().IncreaseMaxHealth (value / numOfHealth);
		}
		return true;
	}
}
