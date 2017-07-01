using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillDescriptionScript : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	}

	public void ShowDescription(RectTransform transform, int flipX, int flipY) {
		flipX = Mathf.Clamp (flipX, -1, 1);
		flipY = Mathf.Clamp (flipY, -1, 1);

		if (flipX == 0) {
			flipX = 1;
		}
		if (flipY == 0) {
			flipY = 1;
		}

		Vector3 targetPosition;
		targetPosition = new Vector3(
			(transform.rect.width * 1.5f  + transform.GetChild(0).GetComponent<RectTransform>().rect.width ) * flipX, 
			-(transform.rect.height * 0.5f + transform.GetChild(0).GetComponent<RectTransform>().rect.height * flipY)  
		);
		this.transform.position = transform.position + targetPosition;
		this.gameObject.SetActive (true);
	}

	public void HideDescription() {
		this.gameObject.SetActive (false);
	}
}
