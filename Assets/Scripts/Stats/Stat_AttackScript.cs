using UnityEngine;
using System.Collections;

public class Stat_AttackScript : MonoBehaviour {

    [SerializeField]
    private int baseAttack = 10;
    [SerializeField]
    private Weapon_Stat WeaponStat_Attack;

    void Start()
    {
    }

    public int TotalAttack()
    {
		baseAttack += WeaponStat_Attack.Attack_Stat;
		return baseAttack;
    }

	public int GetBaseAttackDamage() {
		return baseAttack;
	}
}
