using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialOneEventScript : MonoBehaviour {

    bool startCutScene = false;
    bool entered = false;

    public Light directionalLight;
    public SpriteRenderer backgroundSprite;

    public Color directionalLightOrigin;
    public Color lightTarget;

    public Color backgroundSpriteOrigin;
    public Color backgroundSpriteTarget;

    public Text myText;
    int textIndex = 0;
    public string[] dialogue;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(startCutScene)
        {
            if(GameManager.instance.player == null)
            {
                GameManager.instance.LoadPlayer();
                if(GameManager.instance.player == null)
                {
                    GameManager.instance.player = GameObject.FindGameObjectWithTag("Player");
                }
            }
            StartCoroutine(StartCutScene());
            startCutScene = false;
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !entered)
        {
            startCutScene = true;
            entered = true;
        }

    }

    IEnumerator StartCutScene()
    {
        yield return new WaitForSeconds(0.5f);
        directionalLight.color = lightTarget;
        backgroundSprite.color = backgroundSpriteTarget;
        yield return new WaitForSeconds(0.5f);
        directionalLight.color = directionalLightOrigin;
        backgroundSprite.color = backgroundSpriteOrigin;
        GameManager.instance.player.GetComponent<Player_Input>().StopMovement();
        GameManager.instance.player.GetComponent<Player_Input>().enabled = false;
        yield return new WaitForSeconds(3f);

        myText.gameObject.SetActive(true);
        myText.text = dialogue[textIndex++];
        yield return new WaitForSeconds(2f);
        myText.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        myText.gameObject.SetActive(true);

        myText.text = dialogue[textIndex++];
        yield return new WaitForSeconds(2f);
        myText.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        myText.gameObject.SetActive(true);

        myText.text = dialogue[textIndex++];
        yield return new WaitForSeconds(2f);
        myText.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        GameManager.instance.player.GetComponent<Player_Input>().enabled = true;
    }
}
