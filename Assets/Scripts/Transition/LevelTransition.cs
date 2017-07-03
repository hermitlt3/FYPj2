using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour {

    public static LevelTransition instance;


    public Stat_HealthScript Boss;
    public string MapName;
    public bool isTriggered;

    private float TimeDelay = 0.0f;

    void Start()
    {
        isTriggered = false;
        TimeDelay = 0.0f;
    }

    void Update()
    {
        if (!Boss.isAlive() && !isTriggered)
        {
            TimeDelay++;
        }
        if (TimeDelay >= 200.0f && !isTriggered)
        {
            isTriggered = true;
            RunOnce();
            TimeDelay = 0.0f;
        }
    }

    void RunOnce()
    {
        StartCoroutine(SceneTransitManager.instance.ChangeScene(MapName));
    }
}
