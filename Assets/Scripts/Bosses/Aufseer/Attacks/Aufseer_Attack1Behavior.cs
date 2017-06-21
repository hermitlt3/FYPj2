using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aufseer_Attack1Behavior : MonoBehaviour {

	private Rigidbody2D myRigidbody;
	[SerializeField]
	private float projectileSpeed = 400f;
	[SerializeField]
	private float timeSpan = 10f;
	public int direction;
	private Stat_AttackScript attackDamage;

	// Use this for initialization
	void Start () {
		myRigidbody = GetComponent<Rigidbody2D> ();
		attackDamage = GetComponent<Stat_AttackScript> ();
		//target = GameObject.FindGameObjectWithTag ("Player");
	}

	// Update is called once per frame
	void Update () {
		GetComponent<SpriteRenderer> ().flipX = (direction == 1) ? false : true;

		timeSpan = Mathf.Max (0, timeSpan - Time.deltaTime);
		if (timeSpan <= 0) {
			this.gameObject.SetActive (false);
			Destroy (this);
		}
	}

	void FixedUpdate() {
		myRigidbody.velocity = new Vector2(direction * projectileSpeed, 0) * Time.deltaTime;
	}
		
	void OnCollisionEnter2D(Collision2D other) {

		if (other.collider.gameObject.tag == "Player") {

			other.collider.gameObject.GetComponent<Stat_HealthScript> ().DecreaseHealth (attackDamage.GetBaseAttackDamage ());
			TextPopupManager.instance.ShowTextPopup (GameObject.FindGameObjectWithTag ("PlayerCanvas").GetComponent<Canvas>(), transform.position, attackDamage.GetBaseAttackDamage ().ToString(), TextPopupManager.TEXT_TYPE.DAMAGE);

			this.gameObject.SetActive (false);
			Destroy (this);
		}

		else if (other.collider.gameObject.layer == LayerMask.NameToLayer ("Terrain")) {
			this.gameObject.SetActive (false);
			Destroy (this);

		} 
	}
}
