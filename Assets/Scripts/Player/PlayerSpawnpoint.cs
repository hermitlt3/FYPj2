using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnpoint : MonoBehaviour {

    Vector2 spawnLocation;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y < -50f)
        {
            transform.position = spawnLocation;
        }
	}

    public void SetSpawnLocation(Vector2 spawnPoint)
    {
        if(spawnLocation != spawnPoint)
            spawnLocation = spawnPoint;
    }
}
