using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullScreenScript : MonoBehaviour {

    RectTransform rectTransform;
    Vector2 size;
        // Use this for initialization

    void Start () {
        rectTransform = GetComponent<RectTransform>();
        size = new Vector2(rectTransform.rect.width, rectTransform.rect.height);
        transform.localScale = new Vector2(Mathf.CeilToInt(Screen.width / size.x), Mathf.CeilToInt(Screen.height / size.y));
	}
	
	// Update is called once per frame
	void Update () {
     
    }
}
