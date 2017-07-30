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
		gameObject.SetActive (true);
    }

    public void HideDescription() {
		gameObject.SetActive (false);
	}
}
