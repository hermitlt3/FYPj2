using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeSlimeArea_Script : MonoBehaviour {

    public GameObject fakeBoss;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            fakeBoss.SetActive(true);
            fakeBoss.GetComponent<FakeSlime_Logic>().toBeDisabled = false;
            fakeBoss.GetComponent<FakeSlime_Logic>().ResetSlime();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            fakeBoss.GetComponent<FakeSlime_Logic>().toBeDisabled = true;
        }
    }
}
