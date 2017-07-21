using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class LevelIniter : MonoBehaviour {

    public Vector3 playerStartPosition;
    public GameObject mainCamera;
    public GameObject player;
    public Sprite backgroundSprite;
    public TimelineAsset playerTimeline;

	// Use this for initialization
	void Start () {
	    if(mainCamera == null)
        {
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }	
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        player.transform.position = playerStartPosition;
        player.GetComponent<Player_Spawnpoint>().SetSpawnLocation(playerStartPosition);

        mainCamera.GetComponent<Camera2DFollow>().enabled = false;
        mainCamera.transform.position = new Vector3(playerStartPosition.x, playerStartPosition.y, mainCamera.transform.position.z);
        mainCamera.GetComponent<Camera2DFollow>().enabled = true;

        if(backgroundSprite)
        {
            mainCamera.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = backgroundSprite;
        }

        Destroy(this.gameObject);
	}
}
