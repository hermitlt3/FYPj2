using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat_AttackScript : MonoBehaviour {

    [SerializeField]
    private int BaseAttack = 10;

    public int TotalAttack(int WeaponAttack)
    {
        BaseAttack  += WeaponAttack;
        return BaseAttack;
    }
}
