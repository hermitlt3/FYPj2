using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    GameObject mainPlayer;

    // Check if the player first time to this checkpoint
    bool hasBeenActivated;

    [SerializeField]
    float yOffset = 5f;

    // Use this for initialization
    void Awake()
    {
        mainPlayer = GameObject.FindGameObjectWithTag("Player");
    }

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
            if (other.transform.gameObject.tag == "Player")
            {
                hasBeenActivated = true;
                mainPlayer.SendMessage("SetSpawnLocation", new Vector2(transform.position.x, transform.position.y + yOffset));
            }
        }
    }
}
