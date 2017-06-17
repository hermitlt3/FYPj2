using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblePoolScript : ObjectPoolScript {

	void Awake() {
		if (instance) {
			Destroy (instance);
			return;
		}

		instance = this;
	}

	// Use this for initialization
	protected override void Start () {
		pooledObjects = new List<GameObject> ();
		for (int i = 0; i < poolAmount; i++) {
			GameObject obj = (GameObject)Instantiate (pooledObject);
			obj.transform.parent = this.transform;
			obj.SetActive (false);
			pooledObjects.Add (obj);
		}
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
	}

	public override GameObject GetPooledObject() {

		for (int i = 0; i < pooledObjects.Count; i++) {
			if (!pooledObjects [i].activeInHierarchy) {
				return pooledObjects[i];
			}
		}
		if (willIncrease) {
			GameObject obj = (GameObject)Instantiate (pooledObject);
			pooledObjects.Add (obj);
			return obj;
		}
		return null;
	}
}
