using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuIniter : MonoBehaviour {

    public Slider BGMSlider;
    public Slider SFXSlider;
    public Slider MasterSlider;

	// Use this for initialization
	void Start () {
        BGMSlider.value = AudioManager.instance.GetBGMSound();
        SFXSlider.value = AudioManager.instance.GetSFXSound();
        MasterSlider.value = AudioManager.instance.GetMasterSound();
        Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
