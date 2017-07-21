using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aufseer_AI : Boss_AI {

	private int numberOfAttackPatterns;
	[SerializeField]
	private float[] skillsChance;
	private float selfTimer;

	private GameObject target;
	private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    // Use this for initialization
    // Use this for initialization
    protected override void Start () {
		base.Start ();
		numberOfAttackPatterns = 3;
		currentAttackPattern = -1;

		if (skillsChance.Length != numberOfAttackPatterns) {
			print ("Wrong number of skills");
		} 
		selfTimer = 0f;
		target = GameObject.FindGameObjectWithTag ("Player");

		maxTimeBetweenIntervals = timeBetweenIntervals;
    }
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
		if (!GetComponent<Stat_HealthScript> ().isAlive ()) {
			animator.SetBool ("Dead", true);
		} else {
			TimerUpdate ();
			AnimationUpdate ();
		}
	}

	void AnimationUpdate() {
		if (selfTimer >= timeBetweenIntervals) {
			float randomSkill = Random.Range (0f, 1f);
			float chosenSkill = 0f;
			float minValue = 0f;
			for (int i = 0; i < numberOfAttackPatterns; ++i) {
				chosenSkill += skillsChance [i];
				if (randomSkill >= minValue && randomSkill < chosenSkill) {
					currentAttackPattern = i;
					break;
				}
				minValue += skillsChance[i];
			}
			animator.SetInteger ("AttackStyle", currentAttackPattern);
			selfTimer = 0f;
		}
	}

	void AttackUpdate() {
		if (playerDependentAttacks) {

		} else {
			switch (currentAttackPattern) {
			case 0:
				Aufseer_Attack0 attackOne = this.gameObject.AddComponent<Aufseer_Attack0> ();
				attackOne.SetTarget (target);
				break;
			case 1:
				Aufseer_Attack1 attackTwo = this.gameObject.AddComponent<Aufseer_Attack1> ();
				attackTwo.SetTarget (target);
				break;
			case 2:
				Aufseer_Attack2 attackThree = this.gameObject.AddComponent<Aufseer_Attack2> ();
				attackThree.SetTarget (target);
				break;
			}
			currentAttackPattern = -1;
		}
	}

	void TimerUpdate() {
		selfTimer = Mathf.Min(selfTimer + Time.deltaTime, timeBetweenIntervals);
	}

	public override void Reset() {
        if(!this.gameObject.activeInHierarchy)
        {
            return;
        }
        animator.SetTrigger("Reset");
        animator.SetBool("Dead", false);
        for (int i = 0; i < transform.childCount; ++i)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        shouldDie = false;
        GetComponent<Stat_HealthScript>().SetCurrentHealth(GetComponent<Stat_HealthScript>().GetMaxHealth());
    }

    void ReleaseStuff () {
        if(!shouldDie)
        {
            return;
        }

        CollectiblesGenerator.instance.GenerateCollectibles (transform.position, 100, 20);
		Destroy (this.gameObject.GetComponentInChildren<Canvas> ().gameObject);
	}

    void ShouldDie()
    {
        if(GameObject.FindGameObjectWithTag("Player").GetComponent<Stat_HealthScript>().isAlive() == false)
        {
            shouldDie = false;
        } else
        {
            shouldDie = true;
        }
    }

    public void ResetTimer (float timer = 0f) {
		selfTimer = timer;
	}
}
