using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageAI_AttackBehaviour : MonoBehaviour {

	private Rigidbody2D myRigidbody;

	[SerializeField]
	private float projectileSpeed = 500f;
	[SerializeField]
	private float lifeSpan = 5f;

	private Vector2 direction;
	private GameObject target;
	private Stat_AttackScript attackDamage;

	// Use this for initialization
	void Start () {
		myRigidbody = GetComponent<Rigidbody2D> ();
		attackDamage = GetComponent<Stat_AttackScript> ();
		if (target) {
			direction = target.transform.position - transform.position;
		}
		transform.rotation = Quaternion.Euler (new Vector3 (0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg));
	}

	// Update is called once per frame
	void Update () {
		lifeSpan = Mathf.Max (0, lifeSpan - Time.deltaTime);
		if (lifeSpan <= 0) {
			this.gameObject.SetActive (false);
			Destroy (this);
		}
	}

	void FixedUpdate () {
		myRigidbody.velocity = direction.normalized * projectileSpeed * Time.deltaTime;
	}

	public bool SetTarget(GameObject obj) {
		target = obj;
		return true;
	}

	public bool SetDirection(Vector2 dir) {
		direction = dir;
		return true;
	}

	void OnCollisionEnter2D(Collision2D other) {

		if (other.collider.gameObject.CompareTag("Player")) {
			other.collider.gameObject.GetComponent<Stat_HealthScript> ().DecreaseHealth (attackDamage.GetBaseAttackDamage ());
			TextPopupManager.instance.ShowTextPopup (GameObject.FindGameObjectWithTag ("PlayerCanvas").GetComponent<Canvas>(), other.collider.transform.position, "-" +attackDamage.GetBaseAttackDamage ().ToString(), TextPopupManager.TEXT_TYPE.DAMAGE);

			this.gameObject.SetActive (false);
			Destroy (this);
		}

		else if (other.collider.gameObject.layer == LayerMask.NameToLayer ("Terrain")) {
			this.gameObject.SetActive (false);
			Destroy (this);

		} 
	}
}
