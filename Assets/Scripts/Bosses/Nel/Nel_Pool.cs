using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nel_Pool : MonoBehaviour {

    public GameObject prefab;

    public bool willIncrease = true;
    public int poolAmount = 20;

    protected List<GameObject> pooledObjects;

    // Use this for initialization
    void Start()
    {
        pooledObjects = new List<GameObject>();
        for (int j = 0; j < poolAmount; j++)
        {
            GameObject obj = Instantiate(prefab);
            obj.transform.SetParent(transform, false);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    // Update is called once per frame
   void Update()
   {

   }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        if (willIncrease)
        {
            GameObject obj = Instantiate(prefab);
            obj.transform.SetParent(transform, false);
            pooledObjects.Add(obj);
            return obj;
        }
        return null;
    }

    public void DeactivateAll()
    {
        for (int j = 0; j < poolAmount; j++)
        {
            pooledObjects[j].SetActive(false);
        }
    }
}
