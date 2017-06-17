using UnityEngine;
using System.Collections;

public class Player_Experience : MonoBehaviour {

    [SerializeField]
    private float experience = 0;
    [SerializeField]
    private float maxExperience = 100;
    [SerializeField]
    private int level = 1;
	[SerializeField]
	private float expIncrement = 1.6f;

	private Stat_AbilityPoint abilityPoint;

    void Start()
	{
		abilityPoint = GetComponent<Stat_AbilityPoint> ();
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
		maxExperience  = maxExperience * expIncrement;
		abilityPoint.IncreaseAbilityPoint (1);
	}

	public void IncreaseExperience(float value) {
		experience = Mathf.Max(0, experience + value);
	}

	public void DecreaseExperience(float value) {
		experience = Mathf.Max(0, experience - value);
	}

	public float GetExperience() {
		return experience;
	}
}
