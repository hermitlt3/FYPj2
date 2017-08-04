using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitSceneAreaScript : MonoBehaviour {

    public string nextLevelName;    // Next level name     

    AudioSource theSound;           // In case we want a sound
    public bool playSoundOnce;
    bool hasSoundPlayed;

    // Use this for initialization
    void Start()
    {
        theSound = GetComponent<AudioSource>();
        hasSoundPlayed = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            if (playSoundOnce)
            {
                if (!hasSoundPlayed)
                {
                    hasSoundPlayed = true;
                    AudioManager.instance.PlaySound(theSound, 0f);
                }
            }
            else
            {
                AudioManager.instance.PlaySound(theSound, 0f);
            }
            StartCoroutine(SceneTransitManager.instance.ChangeScene(nextLevelName, 2f));
        }
    }
}
