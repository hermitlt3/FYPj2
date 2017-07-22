using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    // Check if the player first time to this checkpoint
    bool hasBeenActivated;


    [SerializeField]
    float yOffset = 5f;

    // Use this for initialization
    void Start()
    {
        hasBeenActivated = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasBeenActivated)
        {
			if (other.gameObject.CompareTag("Player"))
            {
                hasBeenActivated = true;
                AudioManager.instance.PlaySound(GetComponent<AudioSource>());
				transform.GetChild (0).gameObject.SetActive (true);
				other.gameObject.GetComponent<Player_Spawnpoint>().SetSpawnLocation(new Vector2(transform.position.x, transform.position.y + yOffset));
				TextPopupManager.instance.ShowTextPopup(other.transform.gameObject.GetComponentInChildren<Canvas> (), other.transform.position, "Checkpoint!", TextPopupManager.TEXT_TYPE.HEAL_PLAYER);
            }
        }
    }
}
