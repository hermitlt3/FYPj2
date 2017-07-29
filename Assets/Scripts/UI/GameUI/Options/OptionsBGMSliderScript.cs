using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsBGMSliderScript : MonoBehaviour {

    AudioManager am;
    Slider bgmslider;

    // Use this for initialization
    void Start()
    {
        am = GameObject.FindGameObjectWithTag("Audio Manager").GetComponent<AudioManager>();
        bgmslider = GetComponent<Slider>();
        bgmslider.onValueChanged.AddListener(ValueChangeCheck);
    }

    private void ValueChangeCheck(float arg0)
    {
        am.BGMSetSound(arg0);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
