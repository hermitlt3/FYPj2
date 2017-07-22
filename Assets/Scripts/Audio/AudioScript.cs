using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioScript : MonoBehaviour
{
    public AudioMixer masterMixer;

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
}
