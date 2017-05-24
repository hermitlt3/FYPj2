using UnityEngine;
using System.Collections;

public class Player_Stat : MonoBehaviour {


    [SerializeField]
    private int Stat_Attack;
    [SerializeField]
    private float Stat_AttackSpd;
    [SerializeField]
    private float Stat_Critical;

    [SerializeField]
    private Stat_AttackScript Attack;
    [SerializeField]
    private Stat_AttackSpdScript AttackSpd;
    [SerializeField]
    private Skill_CritStat Critical;

    void Start()
    {
        Stat_Attack = Attack.TotalAttack();
        Stat_AttackSpd = AttackSpd.TotalAtkSpd();
        //Stat_Critical = Critical.TotalCrit();
    }

}
