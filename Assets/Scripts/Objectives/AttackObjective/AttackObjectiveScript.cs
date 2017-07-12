using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackObjectiveScript : BaseObjectiveScript {

    public float animSpeed = 0.5f;
    Animator thisAnimator;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        description = "Left click to attack or interact";

        GetComponent<Text>().text = description;

        thisAnimator = GetComponent<Animator>();
        thisAnimator.SetFloat(Animator.StringToHash("FadeSpeed"), animSpeed);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if ((Input.GetAxisRaw("Fire1") == 1) && thisAnimator.GetBool(Animator.StringToHash("FadeOut")) == false)
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
