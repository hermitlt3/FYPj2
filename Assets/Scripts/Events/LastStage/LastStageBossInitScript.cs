using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastStageBossInitScript : MonoBehaviour {

    public GameObject enemyList;
    public Vector3 playerSpawnPosition;
    public GameObject boss;

    bool playFade;
    bool playAnimation;
	// Use this for initialization
	void Start () {
        playAnimation = playFade = false;
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

        GameManager.instance.player.transform.position = playerSpawnPosition;
        GameManager.instance.player.GetComponent<Player_Input>().enabled = false;
        GameManager.instance.player.GetComponent<SpriteRenderer>().flipX = true;

        fadeTime = SceneTransitManager.instance.FadeIn(0.6f);
        yield return new WaitForSeconds(fadeTime);

        yield return new WaitForSeconds(delay);
        playAnimation = true;
    }
}
