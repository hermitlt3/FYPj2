using UnityEngine;
using System.Collections;

public class Stat_HealthScript : MonoBehaviour {

    [SerializeField]
    private float maxHealth = 100.0f;
    [SerializeField]
    private float currHealth;
	public float bonusHealth = 0f;

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
		if (currHealth > 0)
			return true;
		else
			return false;
	}

	public float GetCurrentHealth() 
	{
		return currHealth;
	}

	public float GetMaxHealth()
	{
		return maxHealth;
	}

	public void IncreaseMaxHealth(float value) 
	{
		maxHealth = Mathf.Max (0, value);
	}

	public void SetCurrentHealth(float value)
	{
		currHealth = Mathf.Max (0, value);
	}
}
