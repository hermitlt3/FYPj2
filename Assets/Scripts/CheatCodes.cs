using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatCodes : MonoBehaviour {

    private string[] cheatCode;
    private int index;

    void Start()
    {
        cheatCode = new string[] { "c","h","e","a","t" };
        index = 0;
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
                Destroy(gameObject);
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
        }

       
    }
}
