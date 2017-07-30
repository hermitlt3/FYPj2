using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalToNextLevel : MonoBehaviour {

    public Boss_AI theBoss;
    public string nextLevelName = "LastScene";
    Animator animator;
    bool startAnim;
    bool loaded;
    AudioSource sound;
    bool soundPlayed;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        sound = GetComponent<AudioSource>();
        startAnim = false;
        loaded = false;
        soundPlayed = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (!startAnim && theBoss.shouldDie)
        {
            startAnim = true;
            animator.SetBool("Load", startAnim);
        }

        if(loaded)
        {
            animator.SetBool("Play", loaded);
        }
    }

    void Loaded()
    {
        loaded = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && loaded)
        {
            GameManager.instance.player.GetComponent<Player_Input>().enabled = false;
            StartCoroutine(SceneTransitManager.instance.ChangeScene(nextLevelName));
            if (sound && !soundPlayed)
            {
                sound.Play();
                soundPlayed = true;
            }
        }
    }
}
