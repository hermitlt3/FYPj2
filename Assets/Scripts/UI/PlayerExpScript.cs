using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerExpScript : BarScript {

	private Player_Experience expScript;
	private Text expText;
	private float oldExpPercent;
	private float newExpPercent;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		barImage = GetComponentsInChildren<Image> ()[1];
		expScript = gO.GetComponent<Player_Experience> ();

		expText = GetComponentInChildren<Text> ();

		newExpPercent = (float)(expScript.GetExperience () * 100)/ (float)(expScript.GetMaxExperience ()) ;
		oldExpPercent = newExpPercent;

		expText.text = newExpPercent + "%";
		barImage.fillAmount = newExpPercent * 0.01f ;
	}

	// Update is called once per frame
	protected override void Update () {

		newExpPercent =  (float)(expScript.GetExperience () * 100)/ (float)(expScript.GetMaxExperience ());
		//if (newHealthPercent - oldHealthPercent > Mathf.Epsilon) {
		ExpBarAnimation (oldExpPercent, newExpPercent, 0.1f);
		//}
		oldExpPercent = newExpPercent;
	}

	void ExpBarAnimation(float old, float replace, float time) {
		expText.text = newExpPercent.ToString("F1") + "%";
		//		float difference = (replace - old) * 0.01f;
		//		if (barImage.fillAmount - difference > Mathf.Epsilon) {
		//			float dir = (difference < 0f) ? -1f : 1f;
		//			barImage.fillAmount += newHealthPercent * dir * Time.deltaTime;
		//		}
		barImage.fillAmount = replace * 0.01f;
	}
}
