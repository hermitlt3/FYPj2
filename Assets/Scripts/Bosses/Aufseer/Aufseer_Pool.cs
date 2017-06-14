using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aufseer_Pool : MonoBehaviour {

	public GameObject[] prefabs;

	public bool willIncrease = true;
	public int poolAmount = 15;

	protected List<GameObject>[] pooledObjects;

	// Use this for initialization
	void Start () {
		pooledObjects = new List<GameObject>[prefabs.Length];

		for (int i = 0; i < pooledObjects.Length; i++) {
			pooledObjects[i] = new List<GameObject>();

			for (int j = 0; j < poolAmount; j++) {
				GameObject obj = (GameObject)Instantiate (prefabs[i]);
				obj.transform.parent = this.transform;
				obj.SetActive (false);
				pooledObjects [i].Add (obj);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public GameObject GetPooledObject(int index) {
		
		for (int i = 0; i < pooledObjects [index].Count; i++) {
			if (!pooledObjects [index][i] .activeInHierarchy) {
				return pooledObjects [index][i];
			}
		}
		return null;
	}
}
