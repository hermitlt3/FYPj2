using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationTransit : MonoBehaviour {

    public Animator Clip;

    public string MapName;
    bool isTriggered;
	// Use this for initialization
	void Start () {
        Clip.GetComponent<Animator>();
        isTriggered = false;
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void GoToMainMenu()
    {
        if (!isTriggered)
        {
            StartCoroutine(SceneTransitManager.instance.ChangeScene(MapName));
            isTriggered = true;
        }
    }
}
