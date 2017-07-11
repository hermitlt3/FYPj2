using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementObjectiveScript : BaseObjectiveScript {

    public float animSpeed = 0.5f;
    Animator thisAnimator;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        description = "A and D to move";

        GetComponent<Text>().text = description;

        thisAnimator = GetComponent<Animator>();
        thisAnimator.SetFloat(Animator.StringToHash("FadeSpeed"), animSpeed);
    }
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
        if((Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1) && thisAnimator.GetBool(Animator.StringToHash("FadeOut")) == false)
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
