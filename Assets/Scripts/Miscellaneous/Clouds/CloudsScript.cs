using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudsScript : MonoBehaviour {

    int direction;
    Collider2D end;
    float cloudSpeed = 0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += new Vector3(direction * cloudSpeed * Time.deltaTime, 0);
        if(Mathf.Abs(transform.position.x - end.bounds.center.x) < 0.1f)
        {
            gameObject.SetActive(false);
            Destroy(this);
        }
	}

    public void InitialiseClouds(Collider2D start, Collider2D end, float speed)
    {
        this.end = end;
        transform.position = new Vector3(start.bounds.min.x, Random.Range(end.bounds.min.y, end.bounds.max.y));
        direction = (start.bounds.center.x < end.bounds.center.x) ? 1 : -1;
        cloudSpeed = speed;
    }
}
