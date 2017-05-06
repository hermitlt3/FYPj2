using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

    [SerializeField]
    Player player;
    [SerializeField]
    GameObject experience;
    float timer = 0f;
    float spawntime = 1f;

    public Animator animator;
	// Use this for initialization
	void Start () {
        timer = spawntime;
	}
	
	// Update is called once per frame
	void Update () {
        if (player.GetComponent<BoxCollider2D>().bounds.Intersects(this.GetComponent<BoxCollider2D>().bounds))
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                for (int i = 0; i < 5; ++i)
                {
                    GameObject test = GameObject.Instantiate(experience);
                    timer = spawntime;
                    test.transform.position = transform.position;
                    test.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-8f, 8f), 5);
                }
            }
            animator.SetTrigger("Attack");
        }
        else
        {
            timer = spawntime;
        }
	}
}
