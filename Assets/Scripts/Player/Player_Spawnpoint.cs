using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Spawnpoint : MonoBehaviour {

    Vector2 spawnLocation;
	Collider2D gameArea;

	// Use this for initialization
	void Start () {
		//gameArea = GameObject.FindGameObjectWithTag ("Game Area").GetComponent<Collider2D> ();
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void SetSpawnLocation(Vector2 spawnPoint)
    {
        if(spawnLocation != spawnPoint)
            spawnLocation = spawnPoint;
    }

	public Vector2 GetSpawnLocation()
	{
		return spawnLocation;
	}
}
