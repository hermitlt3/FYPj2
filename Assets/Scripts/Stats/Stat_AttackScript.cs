using UnityEngine;
using System.Collections;

public class Stat_AttackScript : MonoBehaviour {

    [SerializeField]
    private int baseAttack = 10;
	public int bonusAttack = 0;

    void Start()
    {
    }

	public int GetBaseAttackDamage() {
		return baseAttack;
	}

	public void SetAttackDamage(int value) {
		baseAttack = Mathf.Max (0, value);
	}
}
