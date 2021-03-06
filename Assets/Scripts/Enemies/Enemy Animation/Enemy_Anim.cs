﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Anim : MonoBehaviour {

	// In charge of the animations
	protected Animator animator;

	// Health of the enemy
	protected Stat_HealthScript healthScript;

	private EnemyAI_Attack aiAttack;
	private EnemyAI_Move aiMove;
	// Movement speed of the enemy
	protected float movementSpeed;



	protected virtual void Awake () {
		animator = GetComponent<Animator> ();
		healthScript = GetComponent<Stat_HealthScript> ();
		aiAttack = GetComponent<EnemyAI_Attack> ();
		aiMove = GetComponent<EnemyAI_Move> ();
	}

	// Use this for initialization
	protected virtual void Start () {

	}
	
	// Update is called once per frame
	protected virtual void Update () {

		animator.SetFloat ("Attack Speed", aiAttack.GetAttackSpeed ());
		Vector2 velocity = gameObject.GetComponent<Rigidbody2D> ().velocity;
		velocity.y = 0;
		animator.SetFloat ("Speed", velocity.magnitude);

		if (!healthScript.isAlive ()) {
			animator.SetBool ("Dead", true);
		} else {
			if (aiAttack.isAttacking ()) {
				animator.SetBool ("Attacking", true);
			} else if (aiMove.isMoving ()) {
				animator.SetBool ("Attacking", false);
			}
		}
	}

	public virtual void Reset() {
		animator.SetBool ("Dead", false);
		animator.SetTrigger ("Reset");
	}
}
