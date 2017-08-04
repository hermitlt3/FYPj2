using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScalingScript : MonoBehaviour {

    SpriteRenderer sr;
    Camera leCamera;
    // Use this for initialization
    void Start () {
        sr = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        if(leCamera == null)
        {
            leCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }
        float worldScreenHeight = leCamera.orthographicSize * 2;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        transform.localScale = new Vector3(
            worldScreenWidth / sr.sprite.bounds.size.x * 50,
            worldScreenHeight / sr.sprite.bounds.size.y * 50, 1);
    }
}
