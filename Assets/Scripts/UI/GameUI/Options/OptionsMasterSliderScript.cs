using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMasterSliderScript : MonoBehaviour {

    AudioManager am;
    Slider masterSlider;

    // Use this for initialization
    void Start () {
        am = GameObject.FindGameObjectWithTag("Audio Manager").GetComponent<AudioManager>();
        masterSlider = GetComponent<Slider>();
        masterSlider.onValueChanged.AddListener(ValueChangeCheck);
    }

    private void ValueChangeCheck(float arg0)
    {
        am.MasterSetSound(arg0);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
