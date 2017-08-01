using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkSoul_AI : Boss_AI {

	// Use this for initialization
	protected override void Start () {
        shouldDie = false;
	}

    // Update is called once per frame
    protected override void Update () {
        if (!GetComponent<Stat_HealthScript>().isAlive() &&
             !GetComponent<Animator>().GetBool("Dead")) {
            shouldDie = true;
            GetComponent<Animator>().SetBool("Dead", true);
            Destroy(this.gameObject.GetComponentInChildren<Canvas>().gameObject);
        }
    }

    public override void Reset()
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        base.Reset();
    }
}
