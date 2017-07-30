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

	public void ChangeDescription(string levelText, string descText) {
		// first index is level, second index is description
		Text[] theTexts = transform.GetComponentsInChildren<Text>();
		theTexts [0].text = levelText;
		theTexts [1].text = descText;
	}

	public void ShowDescription(RectTransform ltransform, int flipX, int flipY) {
		flipX = Mathf.Clamp (flipX, -1, 1);
		flipY = Mathf.Clamp (flipY, -1, 1);

		if (flipX == 0) {
			flipX = 1;
		}
		if (flipY == 0) {
			flipY = 1;
		}

        float multiplyOffset = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>().scaleFactor * 0.5f;
        Vector3 targetPosition;
		targetPosition = new Vector3(
			(ltransform.rect.width * 1.5f * multiplyOffset + ltransform.GetChild(0).GetComponent<RectTransform>().rect.width * multiplyOffset) * flipX, 
			-(ltransform.rect.height * 0.5f * multiplyOffset + ltransform.GetChild(0).GetComponent<RectTransform>().rect.height * multiplyOffset * flipY)  
		);
		gameObject.SetActive (true);
		gameObject.transform.position = ltransform.position + targetPosition;
    }

    public void HideDescription() {
		this.gameObject.SetActive (false);
	}
}
