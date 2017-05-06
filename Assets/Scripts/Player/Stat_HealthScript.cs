using UnityEngine;
using System.Collections;

public class Stat_HealthScript : MonoBehaviour {

    [SerializeField]
    private float MaxHealth = 100.0f;
    [SerializeField]
    private float CurrHealth = 1.0f;

    public void Recover()
    {
        CurrHealth += 1;
        if (CurrHealth > MaxHealth)
            CurrHealth = MaxHealth;
    }

    public void Damaged()
    {
        CurrHealth -= 1;
        if (CurrHealth < 0)
            CurrHealth = 0;
    }
   
}
