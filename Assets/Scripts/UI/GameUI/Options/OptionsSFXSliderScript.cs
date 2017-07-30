using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsSFXSliderScript : MonoBehaviour {

    AudioManager am;
    Slider sfxslider;

    // Use this for initialization
    void Start()
    {
        am = GameObject.FindGameObjectWithTag("Audio Manager").GetComponent<AudioManager>();
        sfxslider = GetComponent<Slider>();
        sfxslider.onValueChanged.AddListener(ValueChangeCheck);
    }

    private void ValueChangeCheck(float arg0)
    {
        am.SFXSetSound(arg0);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
