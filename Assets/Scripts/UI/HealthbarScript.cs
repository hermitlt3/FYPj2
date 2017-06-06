using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarScript : MonoBehaviour {

	private Image healthBarImage;
	private Stat_HealthScript healthScript;
	// Use this for initialization
	void Start () {
		healthBarImage = GetComponent<Image> ();
		healthScript = GetComponentInParent<Stat_HealthScript> ();
	}
	
	// Update is called once per frame
	void Update () {
		healthBarImage.fillAmount = healthScript.GetCurrentHealth () / healthScript.GetMaxHealth ();
	}
}
