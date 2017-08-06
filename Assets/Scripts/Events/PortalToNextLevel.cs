using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalToNextLevel : MonoBehaviour {

    public string nextLevelName;
    public bool lastLevel;
    public bool loadedManually = false;
    Animator animator;
    bool startAnim;
    bool loaded;
    AudioSource sound;
    bool soundPlayed;
    public Boss_AI boss;
    public bool portalCheck = true;

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
        if (!startAnim && boss.shouldDie)
        {
            startAnim = true;
            if (portalCheck)
            {
                animator.SetBool("Load", startAnim);
            }
        }

        if(loaded && portalCheck)
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
        if (collision.gameObject.CompareTag("Player") && (loaded || loadedManually))
        {
            if (lastLevel)
            {
                GameManager.instance.transitToCredits = true;
                GameManager.instance.BackToScene(nextLevelName, 0, false);
                lastLevel = false;
            }
            else
            {
                GameManager.instance.player.GetComponent<Player_Input>().enabled = false;
                StartCoroutine(SceneTransitManager.instance.ChangeScene(nextLevelName));
            }
            if (sound && !soundPlayed)
            {
                sound.Play();
                soundPlayed = true;
            }
        }
    }
}
