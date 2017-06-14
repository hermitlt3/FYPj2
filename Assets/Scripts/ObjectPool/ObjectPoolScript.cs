using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolScript : MonoBehaviour {

	public static ObjectPoolScript instance;
	public GameObject pooledObject;

	public bool willIncrease = true;
	public int poolAmount = 20;

	protected List<GameObject> pooledObjects;

	// Use this for initialization
	protected virtual void Start () {
		
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		
	}

	public virtual GameObject GetPooledObject() {
		for (int i = 0; i < pooledObjects.Count; i++) {
			if (!pooledObjects [i].activeInHierarchy) {
				return pooledObjects[i];
			}
		}
		return null;
	}

	public void DeactivateAll() {
		for (int i = 0; i < pooledObjects.Count; i++) {
			if (pooledObjects [i].activeInHierarchy) {
				pooledObjects [i].SetActive (false);
			}
		}
	}
}
