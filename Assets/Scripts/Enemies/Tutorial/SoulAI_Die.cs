using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulAI_Die : EnemyAI_Die {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    protected override void Deactivate()
    { 
        base.Deactivate();
        if (youOnlyLiveOnce)
        {
            ReloadCheckpointSystem.RemoveEnemyToReloadList(this.gameObject);
        }
        this.gameObject.SetActive(false);
    }

    protected override void ShouldDie()
    {
        if (!GameObject.FindGameObjectWithTag("Player").GetComponent<Stat_HealthScript>().isAlive())
        {
            shouldDie = false;
        }
    }
}
