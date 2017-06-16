using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat_CritChance : MonoBehaviour {

    [SerializeField]
    private float BaseCritChance = 10.0f;

    private float MaxCritChance = 100.0f;

	void Start() {

	}

    public void BuffCritChance(float Value)
    {
        BaseCritChance += Value;
        if (BaseCritChance >= MaxCritChance)
            BaseCritChance = MaxCritChance;
    }

	public float GetCriticalChance() {
		return BaseCritChance;
	}
}
