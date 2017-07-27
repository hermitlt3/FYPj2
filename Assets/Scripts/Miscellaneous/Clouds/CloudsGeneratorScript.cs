using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudsGeneratorScript : MonoBehaviour {

    public Sprite[] cloudSprites;

    public float minSpawnTime = 0;
    public float maxSpawnTime = 0;

    int cloudIndex = -1;
    int cloudID = 0;
    int maxCloudsNumber = 15;
    float spawnTime = 0;
    List<GameObject> cloudsCreated;

    // Use this for initialization
	void Start () {
        cloudsCreated = new List<GameObject>();
        for(int i = 0; i < maxCloudsNumber; ++i)
        {
            GameObject go = new GameObject("cloud" + cloudID++.ToString(), typeof(SpriteRenderer));
            go.transform.parent = transform;
            go.GetComponent<SpriteRenderer>().sortingLayerName = "Foreground";
            go.GetComponent<SpriteRenderer>().sortingOrder = 10;
            go.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 0.3f);
            go.SetActive(false);
            cloudsCreated.Add(go);
        }
        RandomiseSpawnTime();
	}
	
	// Update is called once per frame
	void Update () {
        spawnTime = Mathf.Max(0, spawnTime - Time.deltaTime);
        if(spawnTime <= 0)
        {
            SpawnClouds();
            RandomiseSpawnTime();
        }
	}

    void RandomiseSpawnTime()
    {
        spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
    }

    void SpawnClouds()
    {
        int tempCloudID = Random.Range(0, cloudSprites.Length);
        if(tempCloudID == cloudIndex)
        {
            if(tempCloudID >= cloudSprites.Length - 1)
            {
                tempCloudID -= 1;
            }
            else
            {
                tempCloudID += 1;
            }
        }

        for (int i = 0; i < cloudsCreated.Count; i++)
        {
            if (!cloudsCreated[i].activeInHierarchy)
            {
                cloudsCreated[i].SetActive(true);
                float scale = Random.Range(1f, 5f);
                cloudsCreated[i].transform.localScale = new Vector3(scale, scale); 
                cloudsCreated[i].GetComponent<SpriteRenderer>().sprite = cloudSprites[tempCloudID];
                cloudsCreated[i].GetComponent<SpriteRenderer>().flipX = ((Random.Range(0, 10) < 5) ? true : false);
                cloudsCreated[i].AddComponent<CloudsScript>().InitialiseClouds(GetComponents< Collider2D > ()[0], GetComponents < Collider2D > () [1], Random.Range(2f, 5f));
                break;
            }
        }

        cloudIndex = tempCloudID;
    }
}
