using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    GameObject mainPlayer;
    Light checkPointLight;

    // Check if the player first time to this checkpoint
    bool hasBeenActivated;
    // Check if the light has been lit
    bool lightIsLit;


    [SerializeField]
    float yOffset = 5f;
    [SerializeField]
    float lightIntensity = 5f;
    [SerializeField]
    float timeToLit = 2f;

    // Use this for initialization
    void Awake()
    {
        mainPlayer = GameObject.FindGameObjectWithTag("Player");
        checkPointLight = transform.GetChild(0).GetComponent<Light>();
    }

    void Start()
    {
        hasBeenActivated = false;
        lightIsLit = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasBeenActivated && !lightIsLit)
        {
            if (checkPointLight.intensity < lightIntensity)
            {
                checkPointLight.intensity += Time.deltaTime * timeToLit;
            }
            else
            {
                checkPointLight.intensity = lightIntensity;
                lightIsLit = true;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasBeenActivated)
        {
            if (other.transform.gameObject.tag == "Player")
            {
                hasBeenActivated = true;
				mainPlayer.GetComponent<Player_Spawnpoint>().SetSpawnLocation(new Vector2(transform.position.x, transform.position.y + yOffset));
				TextPopupManager.ShowTextPopup(other.transform.gameObject.GetComponentInChildren<Canvas> (), transform.position, "Checkpoint!", Color.blue, Color.white);
            }
        }
    }
}
