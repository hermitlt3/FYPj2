using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aufseer_Attack0Behavior : MonoBehaviour {

	private Rigidbody2D myRigidbody;
	[SerializeField]
	private float maxSpeed = 50f;
	[SerializeField]
	private float projectileSpeed = 10f;
	[SerializeField]
	private float tracePeriod = 1.2f;			// For how long it will go in the direction of player
	[SerializeField]
	private float invulerablePeriod = 0.5f;		// When first spawn will it immediately hit player

	private Vector2 direction;
	private GameObject target;
	private Stat_AttackScript attackDamage;

	// Use this for initialization
	void Start () {
		myRigidbody = GetComponent<Rigidbody2D> ();
		attackDamage = GetComponent<Stat_AttackScript> ();
		//target = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (target && tracePeriod > 0f) {
			direction = target.transform.position - transform.position;
		}
		transform.rotation = Quaternion.Euler (new Vector3 (0, 0, Mathf.Atan2(myRigidbody.velocity.y, myRigidbody.velocity.x) * Mathf.Rad2Deg));
		tracePeriod = Mathf.Max (0f, tracePeriod - Time.deltaTime);
		invulerablePeriod = Mathf.Max (0f, invulerablePeriod - Time.deltaTime);
	}

	void FixedUpdate() {
		if (myRigidbody.velocity.magnitude < maxSpeed) {
			myRigidbody.velocity += direction.normalized * projectileSpeed * Time.deltaTime;
		} else {
			Vector2.ClampMagnitude (myRigidbody.velocity, maxSpeed);
		}
	}

	public bool SetTarget(GameObject obj) {
		target = obj;
		return true;
	}

	void OnCollisionEnter2D(Collision2D other) {

		if (other.collider.gameObject.tag == "Player") {
			if (invulerablePeriod > 0f) {
				return;
			}
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

