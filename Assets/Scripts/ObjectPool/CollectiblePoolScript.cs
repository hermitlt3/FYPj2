using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblePoolScript : ObjectPoolScript {

	void Awake() {
		if (instance && instance != this) {
			Destroy (instance);
			return;
		}

		instance = this;
	}

	// Use this for initialization
	protected override void Start () {
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

	protected override void Update () {

	}

	public override GameObject GetPooledObject(int index) {

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

	public void DeactivateAll() {
		for (int i = 0; i < pooledObjects.Length; i++) {
			for (int j = 0; j < poolAmount; j++) {
				pooledObjects[j][i].SetActive (false);
			}
		}
	}
}
