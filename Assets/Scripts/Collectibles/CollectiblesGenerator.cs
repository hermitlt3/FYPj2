using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblesGenerator : MonoBehaviour {

	public static CollectiblesGenerator instance;

	// The number of game objects instantiated = exact value / divider
	[SerializeField]
	private int divider = 2;

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

	public void GenerateCollectibles(Vector3 position, int expValue, int hpValue = 5) {
		GenerateEXP (position, expValue);
		GenerateHealth (position, hpValue);
	}

	public void DeactivateAll() {
		ObjectPoolScript.instance.DeactivateAll ();
	}

	public bool GenerateEXP(Vector3 position, int value) {
		int numOfExp = Mathf.CeilToInt (value / divider);
		if (numOfExp <= 0)
			numOfExp = 1;
		
		for (int i = Mathf.FloorToInt (-numOfExp / 2); i <= Mathf.FloorToInt (numOfExp / 2); ++i) {
			GameObject collectible = ObjectPoolScript.instance.GetPooledObject (0);
			collectible.SetActive (true);
			Vector3 randomness = new Vector3(Random.Range(-2f, 2f), Random.Range(0f, 1f), -0.1f);
			collectible.transform.position = position + randomness;
			collectible.GetComponent<CollectibleBehavior> ().Reset ();

			collectible.GetComponent<Rigidbody2D> ().velocity = new Vector2 ((float)i / (float)numOfExp * velocityMagnitude.x, velocityMagnitude.y);
			if (numOfExp > 1 && i == Mathf.FloorToInt (numOfExp / 2) && value % numOfExp > 0) {
				collectible.GetComponent<Stat_ExperienceScript> ().SetExperience (value % numOfExp);
			} else {
				collectible.GetComponent<Stat_ExperienceScript> ().SetExperience (value / numOfExp);
			}
		}
		return true;
	}

	public bool GenerateHealth(Vector3 position, int value) {
		int numOfHealth = Mathf.CeilToInt (value / divider);
		if (numOfHealth <= 0) {
			numOfHealth = 1;
		}

		for (int i = Mathf.FloorToInt (-numOfHealth / 2); i <= Mathf.FloorToInt (numOfHealth / 2); ++i) {
			GameObject collectible = ObjectPoolScript.instance.GetPooledObject (1);
			collectible.SetActive (true);
			Vector3 randomness = new Vector3(Random.Range(-2f, 2f), Random.Range(0f, 1f), -0.1f);
			collectible.transform.position = position + randomness;
			//collectible.transform.position -= new Vector3 (0, 0, 0.5f);
			collectible.GetComponent<CollectibleBehavior> ().Reset ();

			collectible.GetComponent<Rigidbody2D> ().velocity = new Vector2 ((float)i / (float)numOfHealth * velocityMagnitude.x, velocityMagnitude.y);
			if (numOfHealth > 1 && i == Mathf.FloorToInt (numOfHealth / 2) && value % numOfHealth > 0) {
				collectible.GetComponent<Stat_HealthScript> ().IncreaseMaxHealth (value / numOfHealth);
			} else {
				collectible.GetComponent<Stat_HealthScript> ().IncreaseMaxHealth (value / numOfHealth);
			}
		}
		return true;
	}
}
