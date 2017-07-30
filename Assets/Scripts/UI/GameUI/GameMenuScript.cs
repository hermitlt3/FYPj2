using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameMenuScript : MonoBehaviour {

    AudioManager audioManager;

    public Slider BGMSlider;
    public Slider SFXSlider;

    bool valueSet;
	// Use this for initialization
	void Start () {
        audioManager = GameObject.FindGameObjectWithTag("Audio Manager").GetComponent<AudioManager>();
        valueSet = false;
	}
	
	// Update is called once per frame
	void Update () {
        if(valueSet == false)
        {
            valueSet = true;
            BGMSlider.value = audioManager.GetBGMSound();
            SFXSlider.value = audioManager.GetSFXSound();
        }
        audioManager.BGMSetSound(BGMSlider.value);
        audioManager.SFXSetSound(SFXSlider.value);
    }

    public void SetValues()
    {
        valueSet = false;
    }
}
