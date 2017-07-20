using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreTutorialEventStart : CustomEventBaseScript {

    public GameObject player;
    public UITypewriterScript typeWriter;
    public GameObject[] toBeActivated;
    public GameObject[] toBeDestroyed;
    public GameObject fade;

    public Text theText;

    Animator animator;
    bool fadeText;
    bool fadeBlack;

    // Use this for initialization
    protected override void Start () {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").gameObject;
        }
        player.GetComponent<Player_Input>().enabled = false;
        animator = GetComponent<Animator>();
        animator.SetBool("Start", true);
	}

    // Update is called once per frame
    protected override void Update () {
		if(fadeText && theText.color.a > 0)
        {
            theText.color -= new Color(0, 0, 0, Time.deltaTime);
        }
        if(fadeBlack && fade.GetComponent<Image>().color.a > 0)
        {
            fade.GetComponent<Image>().color -= new Color(0, 0, 0, Time.deltaTime);
        }
    }
    
    void StartTyping(int letters)
    {
        StartCoroutine(typeWriter.RunText(letters));
    }

    void ReverseTyping(int hi)
    {
        if(hi > 0)
        {
            StartCoroutine(typeWriter.RemoveAllTextTimely());
        }
        else
        {
            typeWriter.BackspaceText();
        }
    }

    void ChangeText(string newText)
    {
        typeWriter.ChangeText(newText);
    }

    void ChangeTextColor(string color)
    {
        if(color == "Red")
        {
            theText.color = Color.red;
        }
    }

    void SetTypeWriterTime(float time)
    {
        typeWriter.delayTime = time;
    }

    void FadeWords()
    {
        fadeText = true;
    }

    void FadeBlack()
    {
        fadeBlack = true;
    }

    // The bool to say its over
    void SetFinish()
    {
        isDone = true;
        Destroy(typeWriter);
        player.GetComponent<Player_Input>().enabled = true;
        foreach (GameObject go in toBeActivated)
        {
            go.SetActive(true);
        }
        foreach (GameObject go in toBeDestroyed)
        {
            Destroy(go);
        }
    }
}
