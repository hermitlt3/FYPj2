using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2DResolutions : MonoBehaviour {

	public float targetAspectRatioX = 16f;
	public float targetAspectRatioY = 9f;
    static float time;
    float timer = 3f;
	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
        timer = Mathf.Max(0, timer - Time.deltaTime);
        if(timer <= 0)
        {
            timer = 3f;
           // Debug.Log("WorldPositionToScreen" + Camera.main.WorldToScreenPoint(GameObject.FindGameObjectWithTag("Player").transform.position));
        }
	}
}
