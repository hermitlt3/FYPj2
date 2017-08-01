using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nel_AttackBehaviour : MonoBehaviour {

    public Vector3 direction;

    public float projectileSpeed = 10f;
    public int damage = 10;
    public float invulernableTimer = 3f;
    bool hit = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        invulernableTimer = Mathf.Max(0, invulernableTimer - Time.deltaTime);
	}

    private void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !hit)
        {
            collision.gameObject.GetComponent<Stat_HealthScript>().DecreaseHealth(damage);
            TextPopupManager.instance.ShowEnemyDamageTextPopup(GameObject.FindGameObjectWithTag("PlayerCanvas").GetComponent<Canvas>(), collision.transform.position, damage);
            hit = true;
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(this);
        gameObject.SetActive(false);
        invulernableTimer = 3f;
    }
}
