using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEndEvent : CustomEventBaseScript
{
    public GameObject boss;
	// Use this for initialization
	protected override void Start () {
		
	}

    // Update is called once per frame
    protected override void Update () {
		if(!boss.GetComponent<Stat_HealthScript>().isAlive() && !startEvent)
        {
            startEvent = true;
            StartCoroutine(SceneTransitManager.instance.FadeInAndOut());
        }
	}
}
