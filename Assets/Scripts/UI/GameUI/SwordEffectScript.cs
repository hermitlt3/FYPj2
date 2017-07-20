using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordEffectScript : MonoBehaviour {

    public bool flipX;

	// Use this for initialization
	void Start () {
        GetComponent<SpriteRenderer>().flipX = flipX;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void DestroyItself()
    {
        Destroy(this.gameObject);
    }
}
