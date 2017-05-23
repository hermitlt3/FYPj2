using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour {

    public GameObject cube;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void CreateCriticalBuff()
    {
        cube.AddComponent<Skill_CritStat>();
    }
}
