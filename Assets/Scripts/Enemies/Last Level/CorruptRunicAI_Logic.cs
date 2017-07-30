using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptRunicAI_Logic : EnemyAI_Logic {

    public int objectPoolNumber = 10;
    public float timePerHit = 1f;
    public float projectileSpeed = 10f;

    public GameObject attackPrefab;
    public GameObject boss;

    float attackTimer = 0f;
    List<GameObject> objectPool;

	// Use this for initialization
	override protected void Start () {
        attackTimer = timePerHit;
        objectPool = new List<GameObject>();
        for(int i = 0; i < objectPoolNumber; ++i)
        {
            GameObject go = Instantiate(attackPrefab);
            go.transform.parent = transform;
            go.transform.position = transform.position;
            go.SetActive(false);
            objectPool.Add(go);
        }
        if(boss == null)
        {
            print("No boss");
        }
	}
	
	// Update is called once per frame
	override protected void Update () {
        attackTimer = Mathf.Max(0, attackTimer - Time.deltaTime);
        if(attackTimer <= 0f)
        {
            GameObject go = GetPooledObject();
            go.AddComponent<CorruptRunicBallsBehavior>().target = boss;
            go.GetComponent<CorruptRunicBallsBehavior>().projectileSpeed = projectileSpeed;
            go.transform.position = transform.position;
            go.SetActive(true);
            attackTimer = timePerHit;
        }

        if(!GetComponent<Stat_HealthScript>().isAlive())
        {
            Destroy(gameObject);
        }
	}

    protected override void GetsHit(GameObject player)
    {
       
    }

    public override void Reset()
    {
       
    }

    public virtual GameObject GetPooledObject()
    {
        for (int i = 0; i < objectPool.Count; i++)
        {
            if (!objectPool[i].activeInHierarchy)
            {
                return objectPool[i];
            }
        }
        return null;
    }
}
