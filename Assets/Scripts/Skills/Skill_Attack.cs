using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Attack : MonoBehaviour{

    //public int SkillLevel = 0;
    public bool Unlockable = false;

    public void isClicked(GameObject obj)
    {
        if (obj.GetComponent<Skill_Attack>().Unlockable == false && Unlockable == true)
        {
            //SkillLevel += 1;
            Unlockable = false;
            //obj.GetComponent<Skill_Attack>().SkillLevel += SkillLevel;
            obj.GetComponent<Skill_Attack>().Unlockable = true;
            
        }
    }

}
