using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat_AttackScript : MonoBehaviour {

    [SerializeField]
    private int baseAttack = 10;

    public int TotalAttack(int WeaponAttack)
    {
		baseAttack  += WeaponAttack;
		return baseAttack;
    }

	public int GetBaseAttackDamage() {
		return baseAttack;
	}
}
