using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat_MovementSpdScript : MonoBehaviour {

    [SerializeField]
    private float BaseMovementSpd = 100.0f;

	void Start() {

	}

    public float TotalMovementSpd()
    {
        return BaseMovementSpd;
    }

	public float GetBaseMS() {
		return BaseMovementSpd;
	}

    public void SetBaseMS(float ms)
    {
        BaseMovementSpd = ms;
    }
}
