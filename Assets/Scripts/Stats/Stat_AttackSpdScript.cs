using UnityEngine;
using System.Collections;

public class Stat_AttackSpdScript : MonoBehaviour {

    //For Weapon/Skill

    [SerializeField]
    private float AtkSpd = 0.0f;

    [SerializeField]
    private Weapon_Stat WeaponStat_AttackSpd;

    void Start()
    {
    }

    public float TotalAtkSpd()
    {
        AtkSpd += WeaponStat_AttackSpd.AttackSpd_Stat;
        return AtkSpd;
    }
}
