using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLeverScript : MonoBehaviour {

    public GameObject objPrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(this.gameObject.GetComponent<LeverPuzzleKey>().isDone)
        {
            GameObject temp = Instantiate(objPrefab) as GameObject;
            temp.transform.SetParent(GameObject.FindGameObjectWithTag("Player").gameObject.GetComponentInChildren<Canvas>().transform, false);
            Destroy(this);
        }
	}
}
