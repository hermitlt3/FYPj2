using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatCodes : MonoBehaviour {

    private string[] cheatCode;
    private int index;

    private string[] uncheatCode;
    private int unindex;

    void Start()
    {
        cheatCode = new string[] { "c","h","e","a","t" };
        uncheatCode = new string[] { "u", "n", "c","h","e","a","t" };
        index = unindex = 0;

    }

    void Update()
    {
        if (index == cheatCode.Length)
        {
            if (GameManager.instance.player)
            {
                GameManager.instance.player.GetComponent<Stat_MovementSpdScript>().SetBaseMS(20);
                GameManager.instance.player.GetComponent<Stat_HealthScript>().SetMaxHealth(999);
                GameManager.instance.player.GetComponent<Stat_HealthScript>().SetCurrentHealth(999);
                GameManager.instance.player.GetComponent<Stat_AttackScript>().SetAttackDamage(100);
                index = 0;
            }
        }

        else if (unindex == uncheatCode.Length)
        {
            if (GameManager.instance.player)
            {
                GameManager.instance.player.GetComponent<Stat_MovementSpdScript>().SetBaseMS(8);
                GameManager.instance.player.GetComponent<Stat_HealthScript>().SetMaxHealth(100);
                GameManager.instance.player.GetComponent<Stat_HealthScript>().SetCurrentHealth(Mathf.Min(GameManager.instance.player.GetComponent<Stat_HealthScript>().GetCurrentHealth(), 100));
                GameManager.instance.player.GetComponent<Stat_AttackScript>().SetAttackDamage(8);
                unindex = 0;
            }
        }

        else if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(cheatCode[index]))
            {
                index++;
            }
            else
            {
                index = 0;
            }
            if (Input.GetKeyDown(uncheatCode[unindex]))
            {
                unindex++;
            }
            else
            {
                unindex = 0;
            }
        }

       
    }
}
