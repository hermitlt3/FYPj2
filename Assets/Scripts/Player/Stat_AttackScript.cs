using UnityEngine;
using System.Collections;

public class Stat_AttackScript : MonoBehaviour {

    [SerializeField]
    private int Base_Attack = 10;

    [SerializeField]
    private Weapon_Stat WeaponStat_Attack;

    void Start()
    {
    }

    public int TotalAttack()
    {
        Base_Attack += WeaponStat_Attack.Attack_Stat;
        return Base_Attack;
    }

}
