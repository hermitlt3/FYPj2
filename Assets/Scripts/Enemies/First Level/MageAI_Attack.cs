using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageAI_Attack : EnemyAI_Attack {

	public GameObject prefab;

	protected override void Awake() {
		base.Awake ();
	}

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		attackDamage = GetComponent<Stat_AttackScript> ().GetBaseAttackDamage ();
		attackSpeed = 1f / GetComponent<Stat_AttackSpeedScript> ().GetAttackSpeed ();

		attacking = false;
		animationEnd = false;
	}

	// Update is called once per frame
	protected override void Update () {
		//UpdateAttackSpeedTimer ();

		if (BoxDetectedPlayer () != null) {
			attacking = true;

			if (BoxDetectedPlayer ().gameObject.transform.position.x - transform.position.x < 0) {
				sprite.flipX = true;
			} else {
				sprite.flipX = false;
			}

		} else {
			if (animationEnd) {
				attacking = false;
				animationEnd = false;
			}
		}

		animationEnd = false;
	}

	void FixedUpdate() {
		if (GetComponent<Rigidbody2D> ().velocity.x != 0) {
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, GetComponent<Rigidbody2D> ().velocity.y);
		}
	}

	protected override bool Attack() {

		GameObject temp = ObjectPoolScript.instance.GetPooledObject (2);
		temp.transform.position = transform.position;
		temp.SetActive (true);

		MageAI_AttackBehaviour behav = temp.AddComponent<MageAI_AttackBehaviour> ();
		if (RayDetectedPlayer() != null) {
			behav.SetTarget (RayDetectedPlayer ().gameObject);
		} else {
			behav.SetDirection (new Vector2 ((sprite.flipX) ? -1 : 1, 0));
		}
		return true;
	}
		
	void AnimationEnds() {
		animationEnd = true;
	}

	public override void Reset ()
	{

	}
}
