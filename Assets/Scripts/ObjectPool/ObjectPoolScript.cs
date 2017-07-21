using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolScript : MonoBehaviour {

	public static ObjectPoolScript instance;
	public GameObject[] prefabs;

	public bool willIncrease = true;
	public int poolAmount = 5;

	protected List<GameObject>[] pooledObjects;

	void Awake() {
		if (instance && instance != this) {
			Destroy (instance);
			return;
		}
		instance = this;
	}

	// Use this for initialization
	protected virtual void Start () {
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
            obj.transform.parent = this.gameObject.transform;
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
