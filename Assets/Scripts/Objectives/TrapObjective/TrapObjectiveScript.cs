using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrapObjectiveScript : BaseObjectiveScript {

    public float animSpeed = 0.5f;
    public float displayFor = 2f;

    Animator thisAnimator;
    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        description = "Beware of traps";

        GetComponent<Text>().text = description;

        thisAnimator = GetComponent<Animator>();
        thisAnimator.SetFloat(Animator.StringToHash("FadeSpeed"), animSpeed);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        displayFor = Mathf.Max(0, displayFor - Time.deltaTime);
        if (displayFor <= 0f)
        {
            doneObjective = true;
            thisAnimator.SetBool(Animator.StringToHash("FadeOut"), doneObjective);
        }
    }

    public override void DestroyItself()
    {
        Destroy(this.gameObject);
    }
}
