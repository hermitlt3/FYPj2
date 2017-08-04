using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockHardModeScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (GameManager.timesCompleted > 0)
        {
            GetComponent<Button>().interactable = true;
            GetComponentInChildren<Text>().color = Color.white;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
