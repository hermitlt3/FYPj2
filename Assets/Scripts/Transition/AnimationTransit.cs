using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationTransit : MonoBehaviour {

    public Animator Clip;

    public string MapName;
	// Use this for initialization
	void Start () {
        Clip.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        AnimatorStateInfo StateInfo = Clip.GetCurrentAnimatorStateInfo(0);
        if (StateInfo.IsName("End"))
            StartCoroutine(SceneTransitManager.instance.ChangeScene(MapName));

	}
}
