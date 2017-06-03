using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat_AttackSpeedScript : MonoBehaviour {

    [SerializeField]
    private float BaseAttackSpd = 1.0f;

    public float TotalAttackSpd(float WeaponSpd)
    {
        BaseAttackSpd += WeaponSpd;
        return BaseAttackSpd;
    }

	public float GetBaseAttackSpeed() {
		return BaseAttackSpd;
	}
}
