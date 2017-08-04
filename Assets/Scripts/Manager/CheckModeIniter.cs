using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckModeIniter : MonoBehaviour {

    GameObject player;
    public int health;
    public int hardModeHealth = 1;
    public Text myText;

    // Use this for initialization
    void Start () {
        player = GameManager.instance.player;
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        if (!GameManager.instance.transitToCredits)
        {
            StartCoroutine(Warning());
        }
        else
        {
            Destroy(gameObject);
        }
        if (GameManager.instance.mode == GameManager.GAMEMODE.HARD)
        {
            player.GetComponent<Stat_HealthScript>().SetMaxHealth(hardModeHealth, true);
        }
        else
        {
            player.GetComponent<Stat_HealthScript>().SetMaxHealth(health, true);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Warning() {
        myText.text = "You start with 1 health";
        myText.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        myText.gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
