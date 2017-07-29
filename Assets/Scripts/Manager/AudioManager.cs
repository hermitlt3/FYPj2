using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {
	
	public static AudioManager instance;
    public GameObject tempAudioPrefab;
    public AudioMixer masterMixer;

    void Awake() {
        if (instance && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this);
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator FadeOut(AudioSource audioSource, float fadeTime)
    {
        if(audioSource == null)
        {
            yield return null;
        }

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
        if(audioSource == null)
        {
            return;
        }

        if(audioSource.isPlaying)
        {
            return;
        }
        audioSource.PlayDelayed(delay);
    }

    public void MasterSetSound(float soundLevel)
    {
        masterMixer.SetFloat("Master", soundLevel);
    }

    public void BGMSetSound(float soundLevel)
    {
        masterMixer.SetFloat("BGM", soundLevel);
    }

    public void SFXSetSound(float soundLevel)
    {
        masterMixer.SetFloat("SFX", soundLevel);
    }

    public float GetMasterSound()
    {
        float masterVolume = 0f;
        masterMixer.GetFloat("Master", out masterVolume);
        return masterVolume;
    }

    public float GetBGMSound()
    {
        float bgmVolume = 0f;
        masterMixer.GetFloat("BGM", out bgmVolume);
        return bgmVolume;
    }

    public float GetSFXSound()
    {
        float sfxVolume = 0f;
        masterMixer.GetFloat("SFX", out sfxVolume);
        return sfxVolume;
    }
}
