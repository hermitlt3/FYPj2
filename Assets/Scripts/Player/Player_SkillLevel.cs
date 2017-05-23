using UnityEngine;
using System.Collections;

public class Player_SkillLevel : MonoBehaviour {

    [SerializeField]
    private int SkillLevel = 1;


    public void SkillLevelUp(int LevelUp)
    {
        SkillLevel = SkillLevel + LevelUp;
    }
}
