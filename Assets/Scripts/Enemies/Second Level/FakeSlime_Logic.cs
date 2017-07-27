using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeSlime_Logic : MonoBehaviour {

    public GameObject boss;
    public LayerMask layers;
    public bool toBeDisabled = false;
    public float delayAttackTime;
    public float minRandomTime;
    public float maxRandomTime;

    public float initialLocalShadowScaleX = 0.6f;
    public float minLocalShadowScaleX = 0.3f;
    public float initialShadowAlpha = 0.5f;
    public float shadowScalingSpeed = 0.5f;
    public float shadowAlphaScalingSpeed = 0.5f;
    public float yOffset;

    GameObject target;
    public GameObject shadow;

    Vector3 targetPosition = new Vector3();
    Stat_AttackScript attackScript;

    float selfTimer = 0f;
    bool hit = false;
    bool rayhit = false;
    int damage;

    enum STATE
    {
        AFK,
        HOLD,
        ATTACK
    } STATE currState;

    enum ATTACK_STATE
    {
        LANDING,
        LAND,
        RETREATING,
    }
    ATTACK_STATE attState;
	// Use this for initialization
	void Start () {
        target = GameManager.instance.player;
        if(target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }
        targetPosition = target.transform.position;
        currState = STATE.AFK;
        selfTimer = Random.Range(minRandomTime, maxRandomTime);
        attackScript = GetComponent<Stat_AttackScript>();
        damage = attackScript.GetBaseAttackDamage();

	}
	
	// Update is called once per frame
	void Update () {
        if(boss.activeInHierarchy || !boss.GetComponent<Stat_HealthScript>().isAlive())
        {
            return;
        }
		switch(currState)
        {
            case STATE.AFK:
                selfTimer = Mathf.Max(0, selfTimer - Time.deltaTime);
                if (toBeDisabled)
                {
                    gameObject.SetActive(false);
                }
                if (selfTimer <= 0)
                {
                    currState = STATE.HOLD;
                    targetPosition = target.transform.position;
                    selfTimer = delayAttackTime;
                }
                break;

            case STATE.HOLD:
                selfTimer = Mathf.Max(0, selfTimer - Time.deltaTime);
                RaycastHit2D hitray;
                hitray = Physics2D.Raycast(targetPosition, Vector2.down, 20f, layers);
                if (hitray && hitray.collider.gameObject)
                {
                    if (!rayhit)
                    {
                        shadow.SetActive(true);
                        shadow.transform.position = hitray.point;
                        targetPosition = hitray.point;
                        rayhit = true;
                    }
                    shadow.transform.position = new Vector3(shadow.transform.position.x, hitray.point.y);
                    if (shadow.transform.localScale.x > minLocalShadowScaleX)
                    {
                        shadow.transform.localScale -= new Vector3(shadowScalingSpeed * Time.deltaTime, 0);
                    }
                    shadow.transform.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, Time.deltaTime * shadowAlphaScalingSpeed);
                    transform.GetComponent<SpriteRenderer>().flipX = (shadow.transform.position.x > target.transform.position.x) ? true : false;
                }

                if (selfTimer <= 0)
                {
                    selfTimer = 0.1f;
                    currState = STATE.ATTACK;
                    attState = ATTACK_STATE.LANDING;
                }
                break;

            case STATE.ATTACK:
                AttackAnimation();
                break;
        }
	}

    void AttackAnimation()
    {
        selfTimer = Mathf.Max(0, selfTimer - Time.deltaTime);
        switch (attState) {
            case ATTACK_STATE.LANDING:
                transform.position = targetPosition + new Vector3(0, 10f);
                if (selfTimer <= 0)
                {
                    selfTimer = 1f;
                    attState = ATTACK_STATE.LAND;
                    transform.position = targetPosition + new Vector3(0, 1.5f);
                }
                break;
            case ATTACK_STATE.LAND:
                if (selfTimer <= 0)
                {
                    selfTimer = 0.1f;
                    attState = ATTACK_STATE.RETREATING;
                    transform.position = targetPosition + new Vector3(0, 10f);
                }
                break;
            case ATTACK_STATE.RETREATING:
                if (selfTimer <= 0)
                {
                    selfTimer = Random.Range(minRandomTime, maxRandomTime);
                    currState = STATE.AFK;
                    transform.position = new Vector3(transform.position.x, yOffset);

                    shadow.SetActive(false);
                    shadow.transform.localScale = new Vector3(initialLocalShadowScaleX, 0.1f);
                    Color originalColor = shadow.transform.GetComponent<SpriteRenderer>().color;
                    shadow.transform.GetComponent<SpriteRenderer>().color = new Color(originalColor.r, originalColor.g, originalColor.b, initialShadowAlpha);
                    rayhit = false;
                    hit = false;
                    attState = ATTACK_STATE.LANDING;
                    if(toBeDisabled)
                    {
                        gameObject.SetActive(false);
                        toBeDisabled = false;
                    }
                }
                break;
        }
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

    public void ResetSlime()
    {
        if (attState != ATTACK_STATE.LAND)
        {
            selfTimer = Random.Range(minRandomTime, maxRandomTime);
        }
    }
}
