using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// For tutorial(s)
public class BaseObjectiveScript : MonoBehaviour {

    public string description;      // Its description, and the rendered text
    public bool isFinished;         // The entire objective is done including its aniamtion
    public bool doneObjective;      // If the objective has been did

	// Use this for initialization
	protected virtual void Start () {

        description = "";
        isFinished = false;
        doneObjective = false;

        ObjectivesManager.instance.AddToObjectivesList(this);
    }

    // Update is called once per frame
    protected virtual void Update () {
		
	}

    public virtual void DestroyItself()
    {

    }
}
