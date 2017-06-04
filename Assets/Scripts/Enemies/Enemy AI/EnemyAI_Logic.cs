﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_Logic : MonoBehaviour {

	public enum AI_STATES {
		AI_MOVE,
		AI_ATTACK,
		AI_DIE,
		NUM_AI_STATES,
	}

	private AI_STATES currState;
	private MonoBehaviour[] states;


	// Use this for initialization
	void Start () {
		currState = AI_STATES.AI_MOVE;
		states = new MonoBehaviour[(uint)AI_STATES.NUM_AI_STATES];
		states [(uint)AI_STATES.AI_MOVE] = gameObject.GetComponent<EnemyAI_Move> ();
		states [(uint)AI_STATES.AI_ATTACK] = gameObject.GetComponent<EnemyAI_Attack> ();
	}

	// Update is called once per frame
	void Update () {
		if (states [(uint)currState].enabled == false) {
			states [(uint)currState].enabled = true;
		}
		for (AI_STATES i = 0; i < AI_STATES.NUM_AI_STATES - 1; ++i) {
			if (i != currState) {
				states [(uint)i].enabled = false;
			}
		}

		EnemyAI_Move AI_Move = (EnemyAI_Move)(states [(uint)AI_STATES.AI_MOVE]);
		EnemyAI_Attack AI_Attack = (EnemyAI_Attack)(states [(uint)AI_STATES.AI_ATTACK]);

		switch (currState) {
		case AI_STATES.AI_MOVE: 
			if (!AI_Move.isMoving ()) {
				currState = AI_STATES.AI_ATTACK;
				AI_Attack.SetIsAttacking (true);
			}
			break;
		
		case AI_STATES.AI_ATTACK:
			if (!AI_Attack.isAttacking ()) {
				currState = AI_STATES.AI_MOVE;
				AI_Move.SetIsMoving (true);
			}
			break;
		}
			
		if (!gameObject.GetComponent<Stat_HealthScript> ().isAlive ()) {
			//currState = states[(uint)AI_STATES.AI_ATTACK];
		}
	}

	void GetsHit(GameObject player) {
		if (currState == AI_STATES.AI_MOVE) {
			EnemyAI_Move AI_Move = (EnemyAI_Move)(states [(uint)currState]);
			AI_Move.SetIsMoving (true);
			currState = AI_STATES.AI_ATTACK;
		}
	}
}
