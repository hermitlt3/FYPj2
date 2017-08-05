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
        if(gO == null)
        {
            gO = GameObject.FindGameObjectWithTag("Player");
        }
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
        if (newExpPercent < 15f)
        {
            expText.color = Color.yellow;
        }
        else
        {
            expText.color = Color.black;
        }
        ExpBarAnimation (oldExpPercent, newExpPercent, 0.1f);
		oldExpPercent = newExpPercent;
	}

	void ExpBarAnimation(float old, float replace, float time) {
		expText.text = newExpPercent.ToString("F1") + "%";
		barImage.fillAmount = replace * 0.01f;
	}
}
