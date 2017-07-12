using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulAI_Logic : EnemyAI_Logic {

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        if (fullHealthCheck && gameObject.GetComponent<Stat_HealthScript>().GetCurrentHealth() < gameObject.GetComponent<Stat_HealthScript>().GetMaxHealth())
        {
            fullHealthCheck = false;
            ReloadCheckpointSystem.AddEnemyToReloadList(this.gameObject);
        }

        if (!gameObject.GetComponent<Stat_HealthScript>().isAlive())
        {
            gameObject.GetComponent<SoulAI_Move>().enabled = false;
            gameObject.GetComponent<SoulAI_Die>().enabled = true;
        }
    }

    public override void Reset()
    {
        gameObject.GetComponent<SoulAI_Move>().enabled = true;
        gameObject.GetComponent<SoulAI_Die>().enabled = false;

        fullHealthCheck = true;
    }
}
