using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
public class BaseObjectiveAreaScript : MonoBehaviour {

    public GameObject player;
    public GameObject objPrefab;
    public string customText = "";

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
            if(customText != "")
            {
                temp.transform.gameObject.GetComponent<Text>().text = customText;
            }
            Destroy(gameObject);
        }
    }
}
