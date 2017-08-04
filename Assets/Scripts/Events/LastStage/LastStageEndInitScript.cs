using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LastStageEndInitScript : MonoBehaviour {

    GameObject player;
    Animator animator;
    public GameObject portalRight;
    public GameObject portalLeft;

    Text myText;

    bool portalFinish;
    bool portalInit;

	// Use this for initialization
	void Start () {
        player = GameManager.instance.player;
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        animator = GetComponent<Animator>();

        portalFinish = portalInit = false;
        myText = player.transform.GetChild(0).GetChild(0).GetComponent<Text>();
        myText.color = new Color(myText.color.r, myText.color.g, myText.color.b, 0);
    }
	
	// Update is called once per frame
	void Update () {
        if (portalFinish && !portalInit)
        {
            portalInit = true;

            portalLeft.SetActive(true);
            portalRight.SetActive(true);
            portalRight.GetComponent<PortalToNextLevel>().enabled = true;
            portalLeft.GetComponent<PortalToNextLevel>().enabled = true;
        }
	}

    void UnlockPortals()
    {
        portalFinish = true;
    }

    void UnlockText(int value)
    {
        if (value == 1)
        {
            myText.gameObject.SetActive(true);
        }
        else
        {
            myText.gameObject.SetActive(false);
        }
    }

    void SetText(string text)
    {
        myText.text = text;
    }

    void ChangeColor(string text)
    {
        if (text == "red")
        {
            myText.color = Color.red;
        }
        else if (text == "white")
        {
            myText.color = Color.white;
        }
        else if (text == "green")
        {
            myText.color = Color.green;
        }
    }
}
