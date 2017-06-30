using UnityEngine;
using System.Collections;

public class Player_Experience : MonoBehaviour {

    [SerializeField]
    private int experience = 0;
    [SerializeField]
	private int maxExperience = 100;
    [SerializeField]
    private int level = 1;
	[SerializeField]
	private float expIncrement = 1.6f;
	[SerializeField]
	private int levelOnePercentageLost= 20;
	private int percentageLost = 0;

	private Stat_AbilityPoint abilityPoint;

    void Start()
	{
		abilityPoint = GetComponent<Stat_AbilityPoint> ();
		percentageLost = maxExperience / 100 + levelOnePercentageLost;
	}

	void Update()
	{
		if (experience >= maxExperience) {
			LevelUp ();
		}
	}

	public void LevelUp() {
		experience = Mathf.Max (0, experience - maxExperience);
		level += 1;
		maxExperience  = Mathf.FloorToInt(maxExperience * expIncrement);
		percentageLost = maxExperience / 100 + levelOnePercentageLost;
		abilityPoint.IncreaseAbilityPoint (1);
	}

	public void IncreaseExperience(int value) {
		experience = Mathf.Max(0, experience + value);
	}

	public void DecreaseExperience(int value) {
		experience = Mathf.Max(0, experience - value);
	}

	public int GetExperience() {
		return experience;
	}

	public int GetMaxExperience() {
		return maxExperience;
	}
	// Called when player dies
	public void LoseExperience() {
		experience = Mathf.Max (0, experience - (percentageLost * maxExperience / 100));
	}
}
