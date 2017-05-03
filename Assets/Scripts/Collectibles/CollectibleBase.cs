using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleBase : MonoBehaviour {

    // The visuals and the collectible itself
    CollectibleBehavior theCollectible;

	// Use this for initialization
	void Start () {
        // To get the collectible class for the player in it 
        theCollectible = transform.GetComponent<CollectibleBehavior>();
	}
	
	// Update is called once per frame
	void Update () {

	}
}
