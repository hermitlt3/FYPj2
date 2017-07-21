using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageAI_Attack : EnemyAI_Attack {

	protected override void Awake() {
		base.Awake ();
	}

	// Use this for initialization
	protected override void Start () {
		base.Start ();
	}

	// Update is called once per frame
	protected override void Update () {
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
		temp.transform.position = transform.position + new Vector3(0,0, -1);
		temp.SetActive (true);
        temp.GetComponent<AudioSource>().Play();

        MageAI_AttackBehaviour behav = temp.AddComponent<MageAI_AttackBehaviour> ();
        behav.SetDirection (new Vector2 ((sprite.flipX) ? -1 : 1, 0));
		return true;
	}
		
	void AnimationEnds() {
		animationEnd = true;
	}

	public override void Reset ()
	{

	}
}
