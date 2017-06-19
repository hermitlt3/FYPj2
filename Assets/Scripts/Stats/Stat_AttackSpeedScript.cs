using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat_AttackSpeedScript : MonoBehaviour {

    [SerializeField]
    private float attackSpeed = 1.0f;
	public float bonusAttackSpeed = 0f;

	void Start() 
	{

	}

	public float GetAttackSpeed() {
		return attackSpeed;
	}

	public void SetAttackSpeed(float value) {
		attackSpeed = Mathf.Max (0, value);
	}
}
