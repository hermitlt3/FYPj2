using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aufseer_Attack2 : Boss_Attack {

	private Aufseer_Pool pool;
	private int attackPatternIndex = 2;
	private Animator anim;

	public int numOfAttacks = 10;
	private float timer = 0.5f;
	private float fixedTimer;
	private int attackCount;

	// Use this for initialization
	void Start () {
		pool = GetComponent<Aufseer_Pool> ();
		anim = GetComponent<Animator> ();

		if (target == null) {
			target = GameObject.FindGameObjectWithTag ("Player");
		}
		fixedTimer = timer;
		attackCount = 0;
	}

	// Update is called once per frame
	void Update () {
		timer = Mathf.Max (0, timer - Time.deltaTime);

		if (timer <= 0f) {
			GameObject fireBomb = pool.GetPooledObject (attackPatternIndex);
			fireBomb.GetComponent<Aufseer_Attack2Behavior> ().Reset ();
			// this skill requires the script to be in the prefab itself so
			// the animation works, so no addcomponent is needed
			fireBomb.transform.position = target.transform.position;
			fireBomb.SetActive (true);
			timer = fixedTimer;
			attackCount++;
		}
		if (attackCount == numOfAttacks - 1) {
			anim.speed = 1f;
			GetComponent<Aufseer_AI> ().ResetTimer ();
			Destroy (this);
		}
	}

	public void Reset() {
		GetComponent<Animator> ().speed = 0f;
	}
}
