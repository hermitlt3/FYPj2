using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolScript : MonoBehaviour {

	public static ObjectPoolScript instance;
	public GameObject pooledObject;

	public bool willIncrease = true;
	public int poolAmount = 20;

	List<GameObject> pooledObjects;

	protected virtual void Awake() {
		instance = this;
	}
	// Use this for initialization
	protected virtual void Start () {
		pooledObjects = new List<GameObject> ();
		for (int i = 0; i < poolAmount; i++) {
			GameObject obj = (GameObject)Instantiate (pooledObject);
			obj.transform.parent = this.transform;
			obj.SetActive (false);
			pooledObjects.Add (obj);
		}
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		
	}

	public GameObject GetPooledObject() {
		for (int i = 0; i < pooledObjects.Count; i++) {
			if (!pooledObjects [i].activeInHierarchy) {
				return pooledObjects[i];
			}
		}
		if (willIncrease) {
			GameObject obj = (GameObject)Instantiate(pooledObject);
			pooledObjects.Add (obj);
			return obj;
		}
		return null;
	}
}
