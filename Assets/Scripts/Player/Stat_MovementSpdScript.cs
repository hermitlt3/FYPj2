using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat_MovementSpdScript : MonoBehaviour {

    [SerializeField]
    private float BaseMovementSpd = 100.0f;

    public float TotalMovementSpd()
    {
        return BaseMovementSpd;
    }
}
