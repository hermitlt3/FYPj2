using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aufseer_AI : Boss_AI {

	private int numberOfAttackPatterns = 3;

	private float selfTimer = 0f;

	private GameObject target;

	private Animator animator;

	// Use this for initialization
	void Start () {
		numberOfAttackPatterns = 3;
		currentAttackPattern = -1;

		selfTimer = 0f;

		target = GameObject.FindGameObjectWithTag ("Player");

		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		TimerUpdate ();
		AnimationUpdate ();
		AttackUpdate ();
	}

	void AnimationUpdate() {
		if (selfTimer >= timeBetweenIntervals) {
			currentAttackPattern = Random.Range (0, 0);
			animator.SetInteger ("AttackStyle", currentAttackPattern);
			selfTimer = 0f;
		}
	}

	void AttackUpdate() {
		if (playerDependentAttacks) {

		} else {
			switch (currentAttackPattern) {
			case 0:
				Aufseer_Attack0 attackOne = this.gameObject.AddComponent<Aufseer_Attack0> ();
				attackOne.SetTarget (target);
				break;
			}
			currentAttackPattern = -1;
		}
	}

	void TimerUpdate() {
		selfTimer = Mathf.Min(selfTimer + Time.deltaTime, timeBetweenIntervals);
	}
}
