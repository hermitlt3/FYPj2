using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillLevelPopup : MonoBehaviour {

    public GameObject UI;

    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<Stat_AbilityPoint>().GetAbilityPoint() > 0)
        {
            UI.SetActive(true);
        }
        else
        {
            UI.SetActive(false);
        }
    }

}
