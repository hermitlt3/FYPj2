﻿using UnityEngine;
using System.Collections;

public class Stat_HealthScript : MonoBehaviour {

    [SerializeField]
    private float maxHealth = 100.0f;
    [SerializeField]
    private float currHealth;

	void Start () {
		currHealth = maxHealth;
	}

	public void DecreaseHealth(float damage)
    {
		currHealth = Mathf.Max (0, currHealth - damage);
    }

	public void IncreaseHealth(float heal)
    {
		currHealth = Mathf.Min (maxHealth, currHealth + heal);
    }

	public bool isAlive() 
	{
		return (currHealth > 0);
	}

	public float GetCurrentHealth() 
	{
		return currHealth;
	}
}
