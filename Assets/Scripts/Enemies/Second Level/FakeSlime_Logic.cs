using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeSlime_Logic : MonoBehaviour {

    public GameObject boss;
    public LayerMask layers;

    public float delayAttackTime;
    public float trackTimer;

    public float minRandomTime;
    public float maxRandomTime;
    public float yOffset;

    GameObject target;

    Vector3 targetPosition = new Vector3();
    Stat_AttackScript attackScript;

    float selfTimer = 0f;
    bool hit = false;
    bool rayHit = false;
    int damage;

    enum STATE
    {
        AFK,
        TRACK,
        ATTACK
    } STATE currState;

	// Use this for initialization
	void Start () {
        target = GameManager.instance.player;
        if(target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }
        currState = STATE.AFK;
        selfTimer = Random.Range(minRandomTime, maxRandomTime);
        attackScript = GetComponent<Stat_AttackScript>();
        damage = attackScript.GetBaseAttackDamage();

	}
	
	// Update is called once per frame
	void Update () {
        if(boss.activeInHierarchy)
        {
            return;
        }
		switch(currState)
        {
            case STATE.AFK:
                selfTimer = Mathf.Max(0, selfTimer - Time.deltaTime);
                if(selfTimer <= 0)
                {
                    currState = STATE.TRACK;
                    selfTimer = trackTimer;
                }
                break;
            case STATE.TRACK:
                selfTimer = Mathf.Max(0, selfTimer - Time.deltaTime);

                RaycastHit2D hitray;
                hitray = Physics2D.Raycast(target.transform.position, Vector2.down, 20f, layers);

                if (hitray && hitray.collider.gameObject)
                {
                    if (rayHit == false)
                    {
                        rayHit = true;
                    }
                }
                if (selfTimer <= 0)
                {
                    selfTimer = Random.Range(minRandomTime, maxRandomTime);
                    if (hitray && hitray.collider.gameObject)
                    {
                        targetPosition = hitray.transform.position;
                        StartCoroutine(DelaySeconds(delayAttackTime));
                        currState = STATE.ATTACK;
                    } else
                    {
                        currState = STATE.AFK;
                    }
                    rayHit = false;
                }
                break;
            case STATE.ATTACK:
                AttackAnimation();
                break;
        }
	}

    void AttackAnimation()
    {
        transform.position = targetPosition + new Vector3(0, 10f);
        StartCoroutine(DelaySeconds(0.1f));
        transform.position = targetPosition;
        StartCoroutine(DelaySeconds(1f));
        transform.position = targetPosition + new Vector3(0, 10f);
        StartCoroutine(DelaySeconds(0.1f));
        transform.position = new Vector3(targetPosition.x, yOffset);
        currState = STATE.AFK;

        hit = false;
    }

    IEnumerator DelaySeconds(float delay)
    {
        yield return new WaitForSeconds(delay);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !hit)
        {
            hit = true;
            collision.gameObject.GetComponent<Stat_HealthScript>().DecreaseHealth(damage);
            TextPopupManager.instance.ShowEnemyDamageTextPopup(GameObject.FindGameObjectWithTag("PlayerCanvas").GetComponent<Canvas>(), collision.transform.position, damage);
        }
    }
}
