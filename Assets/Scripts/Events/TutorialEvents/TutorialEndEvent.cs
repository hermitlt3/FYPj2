using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This is just a real hardcoded cutscene, no need for capsulating anything cuz the Cinemachine is implemented in Unity 2017 but not here
public class TutorialEndEvent : CustomEventBaseScript
{
    // These are really all the things affected by the cutscene
    public GameObject boss;
    public GameObject player;
    public GameObject[] toBeDestroyed;
    public GameObject[] toBeDeactivated;
    public GameObject mainCanvas;
    public GameObject blocked;
    public Text theText;

    public string sceneName;
    Animator animator;

    // Use this for initialization
    protected override void Start () {
		if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        animator = GetComponent<Animator>();
	}

    // Update is called once per frame
    protected override void Update () {
		if(boss.GetComponent<Boss_AI>().shouldDie && !startEvent)
        {
            if (toBeDestroyed.Length > 0)
            {
                foreach (GameObject go in toBeDestroyed)
                {
                    Destroy(go);
                }
            }
            if (toBeDeactivated.Length > 0)
            {
                foreach (GameObject go in toBeDeactivated)
                {
                    go.SetActive(false);
                }
            }
            StartCoroutine(SceneTransitManager.instance.FadeInAndOut(0, false, true));
            blocked.SetActive(true);
            animator.SetBool("Start", true);
            startEvent = true;
        }
    }

    void UnlockText()
    {
        theText.gameObject.SetActive(true);    
    }

    void UnlockHealth()
    {
        mainCanvas.transform.GetChild(0).gameObject.SetActive(true);
    }

    void UnlockExp()
    {
        mainCanvas.transform.GetChild(1).gameObject.SetActive(true);
    }

    void UnlockSkill()
    {
        mainCanvas.transform.GetChild(2).gameObject.SetActive(true);
    }

    void SetText(string text)
    {
        theText.text = text;
    }

    void DecreasePlayerHealth(int value)
    {
        int health = player.GetComponent<Stat_HealthScript>().GetCurrentHealth();
        if (health - value < 1)
        {
            value = health - value - 1;
            player.GetComponent<Stat_HealthScript>().DecreaseHealth(value);
            return;
        }
        player.GetComponent<Stat_HealthScript>().DecreaseHealth(value);
    }

    void IncreasePlayerHealth(int value)
    {
        player.GetComponent<Stat_HealthScript>().IncreaseHealth(value);
    }

    void IncreasePlayerEXP(int value)
    {
        player.GetComponent<Player_Experience>().IncreaseExperience(value);
    }

    void TransitScene()
    {
        player.GetComponent<Player_Input>().enabled = false;
        StartCoroutine(SceneTransitManager.instance.ChangeScene(sceneName));
    }
}
