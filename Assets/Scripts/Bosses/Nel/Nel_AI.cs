using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nel_AI : Boss_AI {

    Nel_Pool objectPool;
    GameObject target;
    BoxCollider2D arenaBounds;

    float selfTimer;

    public float minProjectileSpeed = 8f;
    public float maxProjectileSpeed = 15f;

    public float minProjectileSize = 0.3f;
    public float maxProjectileSize = 0.8f;

    public int minSpawn = 2;
    public int maxSpawn = 5;

    bool killedPlayer = false;
    int timesKilledPlayer = 0;

    public GameObject EndLevelInit;
    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        shouldReset = false;

        objectPool = GetComponent<Nel_Pool>();
        arenaBounds = GameObject.FindGameObjectWithTag("Boss Arenas").GetComponent<BoxCollider2D>();
        target = GameManager.instance.player;
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").gameObject;
        }
        selfTimer = 0f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (!GetComponent<Stat_HealthScript>().isAlive())
        {
            for (int i = 0; i < transform.childCount; ++i)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            shouldDie = true;
        }
        else
        {
            selfTimer = Mathf.Min(selfTimer + Time.deltaTime, timeBetweenIntervals);
            AttackUpdate();
            if (target.GetComponent<Stat_HealthScript>().isAlive() == false && killedPlayer == false)
            {
                timesKilledPlayer++;
                killedPlayer = true;
            }
        }
    }

    public override void Reset()
    { 
        // 0 is canvas
        for (int i = 1; i < transform.childCount; ++i)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        selfTimer = 0;
        killedPlayer = false;
    }

    void AttackUpdate()
    {
        if (selfTimer >= timeBetweenIntervals)
        {
            int spawn = Random.Range(minSpawn, maxSpawn);
            for (int i = 0; i < spawn; ++i)
            {
                GameObject go = objectPool.GetPooledObject();
                go.transform.position = new Vector3(Random.Range(arenaBounds.bounds.min.x, arenaBounds.bounds.max.x), (Random.Range(0, 2) >= 1) ? arenaBounds.bounds.min.y - 10f : arenaBounds.bounds.max.y + 10f);

                int rngesus = Random.Range(1, 5);
                if (rngesus <= 2)
                {
                    go.AddComponent<Nel_AttackBehaviour>().direction = (target.transform.position - go.transform.position).normalized;
                }
                else
                {
                    go.AddComponent<Nel_AttackBehaviour>().direction = new Vector3(0, (go.transform.position.y < target.transform.position.y) ? 1 : -1);
                }
                go.GetComponent<Nel_AttackBehaviour>().projectileSpeed = Random.Range(minProjectileSpeed, maxProjectileSpeed);
                go.GetComponent<Nel_AttackBehaviour>().damage = GetComponent<Stat_AttackScript>().GetBaseAttackDamage() + GetComponent<Stat_AttackScript>().bonusAttack;

                float size = Random.Range(minProjectileSize, maxProjectileSize);
                go.transform.localScale = new Vector3(size, size);

                go.SetActive(true);
            }
            selfTimer = 0f;
        }
    }

    void ShouldDie()
    {
        if (target.GetComponent<Stat_HealthScript>().isAlive() == false)
        {
            shouldDie = false;
        }
        else
        {
            shouldDie = true;
        }
    }
}
