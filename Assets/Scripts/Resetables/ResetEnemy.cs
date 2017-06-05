using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetEnemy : ResetObject {

	private Color oldColor;

	// Use this for initialization
	protected override void Start () {
		GetComponent<EnemyAI_Logic> ().Reset ();
		GetComponent<EnemyAI_Move> ().Reset ();

		GetComponent<Stat_HealthScript> ().IncreaseHealth (GetComponent<Stat_HealthScript> ().GetMaxHealth ());

		this.gameObject.SetActive (true);

		oldColor =	GetComponent<SpriteRenderer> ().color;
		GetComponent<SpriteRenderer> ().color += new Color (0, 0, 0, 255);

		Destroy (this);
	}
	
	// Update is called once per frame
	protected override void Update () {
		
	}
}
