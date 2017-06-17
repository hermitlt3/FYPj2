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
    private float Stat_MovementSpd;

    [SerializeField]
    private Stat_AttackScript Attack;
    [SerializeField]
	private Stat_AttackSpeedScript AttackSpd;
    //[SerializeField]
    //private Skill_CritStat Critical;
    [SerializeField]
    private Stat_MovementSpdScript MovementSpd;

    void Start()
    {
        Stat_Attack = Attack.TotalAttack();
        Stat_AttackSpd = AttackSpd.GetAttackSpeed();
        //Stat_Critical = Critical.TotalCrit();
        Stat_MovementSpd = MovementSpd.TotalMovementSpd();
    }

}
