using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptRunicBallsBehavior : MonoBehaviour {

    Vector2 direction;
    public float projectileSpeed;
    public GameObject target;

    Rigidbody2D myRigidbody;
	// Use this for initialization
	void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        direction = target.transform.position - transform.position;
	}

    private void FixedUpdate()
    {
        myRigidbody.velocity = direction.normalized * projectileSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == target)
        {
            collision.GetComponent<Nel_Counter>().orbsAbsorbed += 1;
            Destroy(this);
            gameObject.SetActive(false);
        }
    }
}
