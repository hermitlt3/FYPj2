using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimedObjectiveScript : BaseObjectiveScript {

    public float animSpeed = 0.5f;
    public float displayFor = 2f;
    public bool instantStart = false;
    public bool instantEnd = false;
    Animator thisAnimator;
    // Use this for initialization
    protected override void Start()
    {
        isFinished = false;
        doneObjective = false;

        ObjectivesManager.instance.AddToObjectivesList(this);
        thisAnimator = GetComponent<Animator>();
        thisAnimator.SetFloat(Animator.StringToHash("FadeSpeed"), animSpeed);
        if(instantStart)
        {
            doneObjective = true;
            thisAnimator.SetBool(Animator.StringToHash("FadeOut"), doneObjective);
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        displayFor = Mathf.Max(0, displayFor - Time.deltaTime);
        if (displayFor <= 0f)
        {
            doneObjective = true;
            if (!instantEnd)
            {
                thisAnimator.SetBool(Animator.StringToHash("FadeOut"), doneObjective);
            }
            else
            {
                DestroyItself();
            }
        }
    }

    public override void DestroyItself()
    {
        Destroy(gameObject);
    }
}
