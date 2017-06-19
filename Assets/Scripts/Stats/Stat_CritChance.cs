using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat_CritChance : MonoBehaviour {

    [SerializeField]
    private float baseCritChance = 0f;

    private float maxCritChance = 100.0f;

	void Start() {

	}

    public void BuffCritChance(float value)
    {
		baseCritChance = Mathf.Max(0, baseCritChance + value);
		baseCritChance = Mathf.Min(baseCritChance, maxCritChance);
    }

	public float GetCriticalChance() {
		return baseCritChance;
	}

	public void SetCriticalChance(float value) {
		baseCritChance = Mathf.Min(value, maxCritChance);
		baseCritChance = Mathf.Max (0, baseCritChance);
	}
}
