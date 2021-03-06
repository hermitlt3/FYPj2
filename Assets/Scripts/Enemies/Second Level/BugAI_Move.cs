﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugAI_Move : EnemyAI_Move {

    protected override void Awake()
    {
        base.Awake();
    }

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void GetsHit(GameObject gameObject)
    {
        if (this.enabled)
        {
            base.GetsHit(gameObject);
        }
        if (aggressionType == AGGRESSION_TYPE.PASSIVE)
        {
            aggressionType = AGGRESSION_TYPE.AGGRESSIVE;
        }
    }
}
