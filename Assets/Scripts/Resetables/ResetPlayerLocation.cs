using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetPlayerLocation : MonoBehaviour {

    public Vector2 Level1;
    public Vector2 Level2;

    public GameObject Player;

    bool SceneChanged;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (GameObject.Find("FollowCamera2D").GetComponent<LevelTransition>().isTriggered)
        {
            ResetLocation();
            GameObject.Find("FollowCamera2D").GetComponent<LevelTransition>().isTriggered = false;
        }
	}

    public void ResetLocation()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Real")
            Player.transform.position = Level1;
        if (scene.name == "Level 2")
            Player.transform.localPosition = Level2;
    }


}
