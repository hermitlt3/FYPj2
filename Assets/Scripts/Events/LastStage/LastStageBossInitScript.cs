using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LastStageBossInitScript : MonoBehaviour {

    public GameObject enemyList;
    public Vector3 playerSpawnPosition;
    public GameObject boss;
    public Light directionalLight;
    public Collider2D bossArenaCollider;

    bool playFade;
    bool playAnimation;
    Animator animator;
    Text theText;

	// Use this for initialization
	void Start () {
        playAnimation = playFade = false;
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(!CheckEnemies() && playFade == false)
        {
            playFade = true;
            StartCoroutine(StartBossAnimation());
        }
        if(playAnimation)
        {
            boss.GetComponent<SpriteRenderer>().enabled = true;
            boss.GetComponent<Animator>().SetBool("Spawn", playAnimation);
        }
        if(boss.GetComponent<Animator>().GetBool("Ready"))
        {
            if (!animator.enabled)
            {
                animator.enabled = true;
            }
            if (directionalLight.color.g > 0)
            {
                directionalLight.color -= new Color(0, Time.deltaTime, 0);
            }
            GameObject.FindGameObjectWithTag("MainCamera").GetComponentInChildren<SpriteRenderer>().color -= new Color(0, Time.deltaTime, Time.deltaTime, 0);
            boss.GetComponent<SpriteRenderer>().color -= new Color(0, Time.deltaTime, Time.deltaTime, 0);
        }
        if(theText == null)
        {
            theText = GameManager.instance.player.transform.GetChild(0).GetChild(0).GetComponent<Text>();
        }
    }

    bool CheckEnemies()
    {
        foreach (Transform trans in enemyList.transform)
        {
            if(trans.gameObject.activeInHierarchy)
            {
                return true;
            }
        }
        return false;
    }

    IEnumerator StartBossAnimation(float delay = 1f)
    {
        float fadeTime = SceneTransitManager.instance.FadeOut(0.6f);
        yield return new WaitForSeconds(fadeTime);

        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera2DFollow>().enabled = false;
        GameObject.FindGameObjectWithTag("MainCamera").transform.position = new Vector3((boss.transform.position.x + playerSpawnPosition.x) * 0.5f, playerSpawnPosition.y, GameObject.FindGameObjectWithTag("MainCamera").transform.position.z);
        GameManager.instance.player.transform.position = playerSpawnPosition;
        GameManager.instance.player.GetComponent<Player_Input>().enabled = false;
        GameManager.instance.player.GetComponent<Player_Input>().StopMovement();
        GameManager.instance.player.GetComponent<SpriteRenderer>().flipX = true;

        fadeTime = SceneTransitManager.instance.FadeIn(0.6f);
        yield return new WaitForSeconds(fadeTime);

        yield return new WaitForSeconds(delay);
        playAnimation = true;
    }

    void UnlockText(int value)
    {
        if (value == 1)
        {
            theText.enabled = true;
            theText.gameObject.SetActive(true);
        }
        else
        {
            theText.gameObject.SetActive(false);
        }
    }

    void SetText(string text)
    {
        theText.text = text;
    }

    void ChangeColor(string text)
    {
        if (text == "red")
        {
            theText.color = Color.red;
        }
        else if (text == "white")
        {
            theText.color = Color.white;
        }
    }

    void Initialize()
    {
        bossArenaCollider.gameObject.SetActive(true);
        GameManager.instance.player.GetComponent<Player_Input>().enabled = true;
        boss.GetComponent<Boss_AI>().enabled = true;
        boss.GetComponent<Nel_Counter>().AddToDamage();
        boss.layer = LayerMask.NameToLayer("Enemy");
        Destroy(this);
    }
}
