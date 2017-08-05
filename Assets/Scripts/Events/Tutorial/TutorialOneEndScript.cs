using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialOneEndScript : MonoBehaviour
{
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
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (startCutScene)
        {
            StartCoroutine(StartCutScene());
            StartCoroutine(StartBackground());
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
        GameManager.instance.player.GetComponent<Player_Input>().StopMovement(1);
        GameManager.instance.player.GetComponent<Player_Input>().enabled = false;
        GameManager.instance.player.GetComponent<SpriteRenderer>().flipX = true;

        yield return new WaitForSeconds(0.2f);
        directionalLight.color = lightTarget;
        backgroundSprite.color = backgroundSpriteTarget;
        yield return new WaitForSeconds(0.3f);
        directionalLight.color = directionalLightOrigin;
        backgroundSprite.color = backgroundSpriteOrigin;

        yield return new WaitForSeconds(0.2f);

        myText.gameObject.SetActive(true);
        myText.text = dialogue[textIndex++];
        yield return new WaitForSeconds(2f);
        myText.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        myText.color = Color.red;
        myText.gameObject.SetActive(true);

        myText.text = dialogue[textIndex++];
        yield return new WaitForSeconds(2f);
        myText.gameObject.SetActive(false);
        myText.color = Color.white;
        myText.text = "Wait";
        yield return new WaitForSeconds(1f);
    }

    IEnumerator StartBackground()
    {
        yield return new WaitForSeconds(0.2f);
        directionalLight.color = lightTarget;
        backgroundSprite.color = backgroundSpriteTarget;
        yield return new WaitForSeconds(0.5f);
        directionalLight.color = directionalLightOrigin;
        backgroundSprite.color = backgroundSpriteOrigin;

        yield return new WaitForSeconds(0.6f);
        directionalLight.color = lightTarget;
        backgroundSprite.color = backgroundSpriteTarget;
        yield return new WaitForSeconds(1f);
        directionalLight.color = directionalLightOrigin;
        backgroundSprite.color = backgroundSpriteOrigin;

        yield return new WaitForSeconds(0.1f);
        directionalLight.color = lightTarget;
        backgroundSprite.color = backgroundSpriteTarget;
        yield return new WaitForSeconds(0.5f);
        directionalLight.color = directionalLightOrigin;
        backgroundSprite.color = backgroundSpriteOrigin;

        yield return new WaitForSeconds(0.2f);
        directionalLight.color = lightTarget;
        backgroundSprite.color = backgroundSpriteTarget;
        yield return new WaitForSeconds(0.4f);
        directionalLight.color = directionalLightOrigin;
        backgroundSprite.color = backgroundSpriteOrigin;
    }
}
