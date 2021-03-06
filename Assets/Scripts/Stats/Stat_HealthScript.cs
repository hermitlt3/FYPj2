﻿using UnityEngine;
using System.Collections;

public class Stat_HealthScript : MonoBehaviour {

    [SerializeField]
    private int maxHealth = 100;
    [SerializeField]
    private int currHealth;
	public int bonusHealth = 0;

	void Start () {
		currHealth = maxHealth;
	}

	public void DecreaseHealth(int damage)
    {
		currHealth = Mathf.Max (0, currHealth - damage);
    }

	public void IncreaseHealth(int heal)
    {
		currHealth = Mathf.Min (maxHealth + bonusHealth, currHealth + heal);
    }

	public bool isAlive() 
	{
		if (currHealth > 0)
			return true;
		else
			return false;
	}

	public int GetCurrentHealth() 
	{
		return currHealth;
	}

	public int GetMaxHealth()
	{
		return maxHealth + bonusHealth;
	}

	public void IncreaseMaxHealth(int value) 
	{
		maxHealth = Mathf.Max (0, value);
	}

	public void SetCurrentHealth(int value)
	{
		currHealth = Mathf.Max (0, value);
	}

    public void SetMaxHealth(int value, bool reset = false)
    {
        maxHealth = Mathf.Max(0, value);
        if(reset)
        {
            currHealth = maxHealth;
        }
    }
}
