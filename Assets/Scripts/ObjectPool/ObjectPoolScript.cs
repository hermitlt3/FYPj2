using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolScript : MonoBehaviour {

	public static ObjectPoolScript instance;
	public GameObject[] prefabs;

	public bool willIncrease = true;
	public int poolAmount = 20;

	protected List<GameObject>[] pooledObjects;

	// Use this for initialization
	protected virtual void Start () {
		
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		
	}

	public virtual GameObject GetPooledObject(int index) {

		for (int i = 0; i < pooledObjects [index].Count; i++) {
			if (!pooledObjects [index][i] .activeInHierarchy) {
				return pooledObjects [index][i];
			}
		}
		if (willIncrease) {
			GameObject obj = (GameObject)Instantiate (prefabs[index]);
			pooledObjects[index].Add (obj);
			return obj;
		}
		return null;
	}

	public virtual void DeactivateAll() {
		for (int i = 0; i < pooledObjects.Length; i++) {
			for (int j = 0; j < poolAmount; j++) {
				pooledObjects[i][j].SetActive (false);
			}
		}
	}
}
