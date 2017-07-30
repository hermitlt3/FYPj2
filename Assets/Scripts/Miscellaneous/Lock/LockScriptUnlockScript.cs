using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockScriptUnlockScript : InteractiveLock {

    public List<MonoBehaviour> scriptsToUnlock;
    private bool activate;
    // A little delay for our dearest player
    private float delayTimer = 1f;

    // Use this for initialization
    protected override void Start()
    {
        activate = false;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (isTriggered)
        {
            delayTimer = Mathf.Max(0, delayTimer - Time.deltaTime);

            if (activate)
            {
                AudioManager.instance.PlaySound(GetComponent<AudioSource>());
                foreach (MonoBehaviour script in scriptsToUnlock)
                {
                    script.enabled = true;
                }
                activate = false;
            }
        }

        if (delayTimer <= 0f)
        {
            isTriggered = false;
            isDone = true;
        }
    }

    public override void InitVariables()
    {
        isTriggered = true;
        activate = true;
        delayTimer = 1f;
    }
}
