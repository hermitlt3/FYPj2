using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugAI_Die : EnemyAI_Die {

	// Use this for initialization
	override protected void Start () {
        base.Start();
	}

    // Update is called once per frame
    override protected void Update () {
        base.Update();
	}

    protected override void Deactivate() {
        base.Deactivate();
    }

    protected override void ShouldDie() {
        base.ShouldDie();
    }
}
