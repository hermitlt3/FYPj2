using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat_AttackRangeScript : MonoBehaviour {

	[SerializeField]
	private float attackRange = 0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public float GetAttackRange() {
		return attackRange;
	}
}
