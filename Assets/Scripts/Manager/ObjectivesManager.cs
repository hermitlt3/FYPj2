using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectivesManager : MonoBehaviour {

	public static ObjectivesManager instance;

    List<BaseObjectiveScript> objectivesList;

    void Awake()
    {
        if (instance && instance != this)
        {
            Destroy(instance);
            return;
        }
        instance = this;
    }
    // Use this for initialization
    void Start () {
        
        objectivesList = new List<BaseObjectiveScript>();
	}
	
	// Update is called once per frame
	void Update () {
        if (objectivesList.Count > 0)
        {
            foreach (BaseObjectiveScript bs in objectivesList)
            {
                if (bs.isFinished)
                {
                    objectivesList.Remove(bs);
                }
            }
        }
	}

    public bool AddToObjectivesList(BaseObjectiveScript obj) {

        objectivesList.Add(obj);
        return true;
    }
}
