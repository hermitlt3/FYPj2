using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioManager : MonoBehaviour {
	
	public static AudioManager instance;
    public GameObject tempAudioPrefab;

	void Awake() {
		if (instance && instance != this) {
			Destroy (instance);
			return;
		}
		instance = this;
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator FadeOut(AudioSource audioSource, float fadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    public void PlaySound(AudioSource audioSource, float delay = 0f)
    {
        if(audioSource.isPlaying)
        {
            return;
        }
        audioSource.PlayDelayed(delay);
    }
}
