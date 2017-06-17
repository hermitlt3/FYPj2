using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat_AttackSpeedScript : MonoBehaviour {

    [SerializeField]
    private float attackSpeed = 1.0f;

	void Start() 
	{

	}

	public float GetAttackSpeed() {
		return attackSpeed;
	}
}
