using UnityEngine;
using System.Collections;

public class Player_AbilityPoint : MonoBehaviour {

    [SerializeField]
    private Player_Experience SkillPoint;

    [SerializeField]
    private int AttackLevel = 0;
    [SerializeField]
    private int AttackSpdLevel = 0;
    [SerializeField]
    private int CriticalLevel = 0;

    private bool canToggle = false;

    void Update()
    {
        if (SkillPoint.AbilityPoints > 0)
            canToggle = true;
        else
            canToggle = false;
    }

    public void LevelAttack()
    {
        if (canToggle)
        {
            //AttackLevel = AttackLevel + 1;
            SkillPoint.AbilityPoints -= 1;
        }
    }

    public void LevelAttackSpd()
    {
        if (canToggle)
        {
            //AttackSpdLevel = AttackSpdLevel + 1;
            SkillPoint.AbilityPoints -= 1;
        }
    }

    public void LevelCriticalLevel()
    {
        if (canToggle)
        {
          // CriticalLevel = CriticalLevel + 1;
           SkillPoint.AbilityPoints -= 1;
        }
    }
}
