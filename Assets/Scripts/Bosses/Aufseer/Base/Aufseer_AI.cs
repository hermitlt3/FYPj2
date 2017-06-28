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
		animator = GetComponent<Animator> ();

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
				}
				minValue += chosenSkill;
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
				attackThree.Reset ();
				break;
			}
			currentAttackPattern = -1;
		}
	}

	void TimerUpdate() {
		selfTimer = Mathf.Min(selfTimer + Time.deltaTime, timeBetweenIntervals);
	}

	public override void Reset() {
		for (int i = 0; i < transform.childCount; ++i) {
			transform.GetChild(i).gameObject.SetActive(false);
		}
	}

	void DestroyItself () {
		Destroy (this.gameObject);
	}

	public void ResetTimer (float timer = 0f) {
		selfTimer = timer;
	}
}
