using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasImageFullScalerScript : MonoBehaviour {

    Image image;

    // Use this for initialization
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        float worldScreenHeight = Screen.height;
        float worldScreenWidth = Screen.width;

        image.GetComponent<RectTransform>().sizeDelta = new Vector2(worldScreenWidth, worldScreenHeight);
    }
}
