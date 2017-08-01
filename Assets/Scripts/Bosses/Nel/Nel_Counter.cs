using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nel_Counter : MonoBehaviour {

    int maxDamageIncrease = 15;
    public int orbsAbsorbed = 0;

    public int divider = 5;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddToDamage()
    {
        GetComponent<Stat_AttackScript>().bonusAttack = Mathf.Min(orbsAbsorbed / divider, maxDamageIncrease);
        Destroy(this);
    }
}
