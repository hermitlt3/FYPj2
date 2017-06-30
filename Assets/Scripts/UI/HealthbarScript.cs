using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarScript : BarScript {

	private Stat_HealthScript healthScript;
	private Text healthText;
	private float oldHealthPercent;
	private float newHealthPercent;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		barImage = GetComponentsInChildren<Image> ()[1];
		healthScript = player.GetComponent<Stat_HealthScript> ();
		healthText = GetComponentInChildren<Text> ();

		newHealthPercent = (float)(healthScript.GetCurrentHealth () * 100)/ (float)(healthScript.GetMaxHealth () + healthScript.bonusHealth) ;
		oldHealthPercent = newHealthPercent;

		healthText.text = newHealthPercent + "%";
		barImage.fillAmount = newHealthPercent * 0.01f ;
	}
	
	// Update is called once per frame
	protected override void Update () {
		
		newHealthPercent =  (float)(healthScript.GetCurrentHealth () * 100)/ (float)(healthScript.GetMaxHealth () + healthScript.bonusHealth);
		//if (newHealthPercent - oldHealthPercent > Mathf.Epsilon) {
			HealthBarAnimation (oldHealthPercent, newHealthPercent, 0.1f);
		//}
		oldHealthPercent = newHealthPercent;
	}

	void HealthBarAnimation(float old, float replace, float time) {
		healthText.text = newHealthPercent + "%";
//		float difference = (replace - old) * 0.01f;
//		if (barImage.fillAmount - difference > Mathf.Epsilon) {
//			float dir = (difference < 0f) ? -1f : 1f;
//			barImage.fillAmount += newHealthPercent * dir * Time.deltaTime;
//		}
		barImage.fillAmount = replace * 0.01f;
	}
}
