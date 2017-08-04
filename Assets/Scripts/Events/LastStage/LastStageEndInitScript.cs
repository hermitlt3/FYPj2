using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LastStageEndInitScript : MonoBehaviour {

    GameObject player;
    public  GameObject boss;
    public GameObject portalRight;
    public GameObject portalLeft;
    public string[] dialogue;

    Animator animator;
    Text myText;
    float myTimer = 2f;

    int textIndex = 0;

    enum CUTSCENE_STATE
    {
        BACKGROUND_COLOR_CHANGE,
        LIGHT_AND_CHARACTER_COLOR_CHANGE,
        DIALOGUE,
        CHARACTER_FADE,
        DROP_ITEM,
        PORTAL_UNLOCK
    }
    CUTSCENE_STATE cutsceneState;

    public Light directionalLight;

    // Use this for initialization
    void Start () {
        player = GameManager.instance.player;
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        animator = GetComponent<Animator>();

        cutsceneState = CUTSCENE_STATE.BACKGROUND_COLOR_CHANGE;
        myText = player.transform.GetChild(0).GetChild(0).GetComponent<Text>();
        myText.color = new Color(myText.color.r, myText.color.g, myText.color.b, 0);
    }

    // Update is called once per frame
    void Update()
    {
        switch (cutsceneState)
        {
            case CUTSCENE_STATE.BACKGROUND_COLOR_CHANGE:
                if (GameObject.FindGameObjectWithTag("MainCamera").GetComponentInChildren<SpriteRenderer>().color.g < 1)
                {
                    GameObject.FindGameObjectWithTag("MainCamera").GetComponentInChildren<SpriteRenderer>().color += new Color(0, Time.deltaTime, 0, 0);
                }
                if (GameObject.FindGameObjectWithTag("MainCamera").GetComponentInChildren<SpriteRenderer>().color.b < 1)
                {
                    GameObject.FindGameObjectWithTag("MainCamera").GetComponentInChildren<SpriteRenderer>().color += new Color(0, 0, Time.deltaTime, 0);
                }
                if (GameObject.FindGameObjectWithTag("MainCamera").GetComponentInChildren<SpriteRenderer>().color.g >= 1
                    && GameObject.FindGameObjectWithTag("MainCamera").GetComponentInChildren<SpriteRenderer>().color.b >= 1)
                {
                    animator.speed = 1f;
                    cutsceneState = CUTSCENE_STATE.LIGHT_AND_CHARACTER_COLOR_CHANGE;
                }
                break;

            case CUTSCENE_STATE.LIGHT_AND_CHARACTER_COLOR_CHANGE:
                if (directionalLight.color.g < 175f / 255f)
                {
                    directionalLight.color += new Color(0, Time.deltaTime, 0);
                }
                if (boss.GetComponent<SpriteRenderer>().color.g < 1)
                {
                    boss.GetComponent<SpriteRenderer>().color += new Color(0, Time.deltaTime, 0, 0);
                }
                if (boss.GetComponent<SpriteRenderer>().color.b < 1)
                {
                    boss.GetComponent<SpriteRenderer>().color += new Color(0, 0, Time.deltaTime, 0);
                }
                if (directionalLight.color.g >= 175f / 255f && boss.GetComponent<SpriteRenderer>().color.g >= 1 && boss.GetComponent<SpriteRenderer>().color.b >= 1)
                {
                    animator.speed = 1f;
                    myText.text = dialogue[textIndex];
                    cutsceneState = CUTSCENE_STATE.DIALOGUE;
                }
                break;

            case CUTSCENE_STATE.DIALOGUE:
                myText.gameObject.SetActive(true);
                myText.color = Color.green;
                myTimer = Mathf.Max(0, myTimer - Time.deltaTime);
                if(myTimer <= 0)
                {
                    if (textIndex < dialogue.Length)
                    {
                        myText.text = dialogue[textIndex];
                        myTimer = 2f;
                    }
                    else
                    {
                        animator.speed = 1f;
                        cutsceneState = CUTSCENE_STATE.CHARACTER_FADE;
                    }
                    textIndex++;
                }
                break;

            case CUTSCENE_STATE.CHARACTER_FADE:
                if(boss.GetComponent<SpriteRenderer>().color.a > 0f)
                {
                    boss.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, Time.deltaTime);
                }
                if(boss.GetComponent<SpriteRenderer>().color.a <= 0f)
                {
                    myText.text = "";
                    myText.gameObject.SetActive(false);
                    animator.speed = 1f;
                    cutsceneState = CUTSCENE_STATE.DROP_ITEM;
                    myTimer = 1f;
                    animator.speed = 1f;
                }
                break;

            case CUTSCENE_STATE.DROP_ITEM:
               if(transform.GetChild(0).gameObject.activeInHierarchy == false)
                {
                    transform.GetChild(0).gameObject.SetActive(true);
                    transform.GetChild(0).gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 5);
                }
                myTimer = Mathf.Max(0, myTimer - Time.deltaTime);
                if (myTimer <= 0)
                {
                    cutsceneState = CUTSCENE_STATE.PORTAL_UNLOCK;
                    myTimer = 1f;
                    animator.speed = 1f;
                }
                break;

            case CUTSCENE_STATE.PORTAL_UNLOCK:
                portalLeft.SetActive(true);
                portalRight.SetActive(true);
                myTimer = Mathf.Max(0, myTimer - Time.deltaTime);
                if (myTimer <= 0)
                {
                    portalRight.GetComponent<PortalToNextLevel>().enabled = true;
                    portalLeft.GetComponent<PortalToNextLevel>().enabled = true;
                    animator.speed = 1f;
                    boss.SetActive(false);
                }

                break;
        }
    }

    void DoAction()
    {
        animator.speed = 0f;
    }
}
