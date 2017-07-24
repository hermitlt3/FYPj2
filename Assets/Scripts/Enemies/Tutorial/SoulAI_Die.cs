using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulAI_Die : EnemyAI_Die {

	// Use this for initialization
	protected override void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
	}

    protected override void Deactivate() {
        if (!shouldDie)
        {
            return;
        }
        if (youOnlyLiveOnce)
        {
            ReloadCheckpointSystem.RemoveEnemyToReloadList(this.gameObject);
        }
        this.gameObject.SetActive(false);
    }

    protected override void ShouldDie() {
        base.ShouldDie();
    }
}
