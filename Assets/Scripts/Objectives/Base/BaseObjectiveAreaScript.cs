using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObjectiveAreaScript : MonoBehaviour {

    public GameObject player;
    public GameObject objPrefab;

    bool shownObjective;

    // Use this for initialization
    void Start () {
        shownObjective = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && !shownObjective)
        {
            shownObjective = true;
            GameObject temp = Instantiate(objPrefab) as GameObject;
            temp.transform.SetParent(collision.gameObject.GetComponentInChildren<Canvas>().transform, false);
            Destroy(this.gameObject);
        }
    }
}
