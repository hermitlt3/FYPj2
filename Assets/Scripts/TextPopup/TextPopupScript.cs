using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextPopupScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<RectTransform>().anchoredPosition += new Vector2(0, 75f * Time.deltaTime);
	}
		
	// Called when the animation for the popup ends
	void DeleteItself() {
		Destroy (this.gameObject);
	}
}
