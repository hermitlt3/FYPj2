using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapArrowScript : MonoBehaviour {
	// Rotation of Z axis
	public Vector3 moveDirection;
	// Arrow speed
	public float arrowSpeed = 25f; 
	// Lifetime duration
	public float lifeTime = 2f;
	private float fixedLifeTime;
	// Colliding with what layers
	public LayerMask layerMask;

	private int damage;
	private Rigidbody2D myRigidbody;

	public float invulerablePeriod = 0.5f;
	private float fixedInvulerablePeriod;

	bool canKill = true;
	// Use this for initialization
	void Start () {
		fixedLifeTime = lifeTime;
		fixedInvulerablePeriod = invulerablePeriod;

		moveDirection.Normalize ();
		transform.rotation = Quaternion.Euler (0, 0, Mathf.Rad2Deg * (Mathf.Atan2(moveDirection.y, moveDirection.x)));	

		damage = GetComponent<Stat_AttackScript> ().GetBaseAttackDamage ();

		myRigidbody = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateTimer ();
		UpdateMovement ();
	
	}

	void OnCollisionEnter2D (Collision2D other) {
		if (((1 << other.gameObject.layer) & layerMask) != 0) {
			if (other.gameObject.GetComponent<Stat_HealthScript> () && canKill) {
				other.gameObject.GetComponent<Stat_HealthScript> ().DecreaseHealth (damage);

				TextPopupManager.instance.ShowTextPopup (GameObject.FindGameObjectWithTag ("PlayerCanvas").GetComponent<Canvas> (), other.transform.position, "-" + damage.ToString (), TextPopupManager.TEXT_TYPE.DAMAGE_ENEMY);
				Reset ();
				this.gameObject.SetActive (false);
			}
			if (invulerablePeriod <= 0f && other.gameObject.layer == LayerMask.NameToLayer("Terrain")) {
				myRigidbody.bodyType = RigidbodyType2D.Static;
				canKill = false;
			}
		}
	}

	void UpdateTimer() {
		lifeTime = Mathf.Max (0, lifeTime - Time.deltaTime);
		invulerablePeriod = Mathf.Max (0, invulerablePeriod - Time.deltaTime);

		if (lifeTime <= 0) {
			Reset();
			this.gameObject.SetActive (false);
		}
	}

	void UpdateMovement() {
		if (myRigidbody.bodyType != RigidbodyType2D.Static) {
			myRigidbody.velocity = moveDirection * arrowSpeed;
		}
	}

	void Reset() {
		lifeTime = fixedLifeTime;
		invulerablePeriod = fixedInvulerablePeriod;
		myRigidbody.bodyType = RigidbodyType2D.Dynamic;
		myRigidbody.velocity = Vector3.zero;
		transform.rotation = Quaternion.Euler (0, 0, Mathf.Rad2Deg * (Mathf.Atan2(moveDirection.y, moveDirection.x)));
		canKill = true;
	}
}
