using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialToCreditsScript : MonoBehaviour {

    public Camera2DFollow followCamera;
    public string sceneName = "Credit Scene";
    public Collider2D stopper;
    bool hit;
	// Use this for initialization
	void Start () {
        hit = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !hit)
        {
            if(GameManager.instance.transitToCredits)
            {
                hit = true;
                followCamera.enabled = false;
                stopper.enabled = true;
                collision.gameObject.GetComponent<Player_Input>().enabled = false;
                SceneTransitManager.instance.fading.fadeSpeed = 0.4f;
                GameManager.instance.player = collision.gameObject;
                GameManager.instance.BackToScene(sceneName, 2f);
                GameManager.instance.transitToCredits = false;
                GameManager.timesCompleted++;
            }
        }
    }
}
