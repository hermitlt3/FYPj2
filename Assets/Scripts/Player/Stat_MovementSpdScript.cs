using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat_MovementSpdScript : MonoBehaviour {

    [SerializeField]
    private float BaseMovementSpd = 1.0f;

    public float TotalMovementSpd(float MvSpd)
    {
        BaseMovementSpd += MvSpd;
        return BaseMovementSpd;
    }

	public float GetBaseMS() {
		return BaseMovementSpd;
	}
}
