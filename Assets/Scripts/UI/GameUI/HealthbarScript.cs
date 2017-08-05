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
        if(gO == null)
        {
            gO = GameObject.FindGameObjectWithTag("Player");
        }
		barImage = GetComponentsInChildren<Image> ()[1];
		healthScript = gO.GetComponent<Stat_HealthScript> ();
		healthText = GetComponentInChildren<Text> ();

		newHealthPercent = (float)((healthScript.GetCurrentHealth () + healthScript.bonusHealth )* 100)/ (float)(healthScript.GetMaxHealth () + healthScript.bonusHealth) ;
		oldHealthPercent = newHealthPercent;

		healthText.text = newHealthPercent + "%";
		barImage.fillAmount = newHealthPercent * 0.01f ;
	}
	
	// Update is called once per frame
	protected override void Update () {
		
		newHealthPercent =  (float)(healthScript.GetCurrentHealth () * 100)/ (float)(healthScript.GetMaxHealth ());
		HealthBarAnimation (oldHealthPercent, newHealthPercent, 0.1f);
        if(newHealthPercent < 15f)
        {
            if (gO.CompareTag("Player"))
            {
                healthText.color = Color.green;
            }
            else
            {
                healthText.color = Color.red;
            }
        }
        else
        {
            healthText.color = Color.black;
        }
		oldHealthPercent = newHealthPercent;
	}

	void HealthBarAnimation(float old, float replace, float time) {
		healthText.text = newHealthPercent.ToString("F1") + "%";
		barImage.fillAmount = replace * 0.01f;
	}
}
