using UnityEngine;
using System.Collections;

public class Player_Experience : MonoBehaviour {

    [SerializeField]
    private int Experience = 0;
    [SerializeField]
    private int MaxExperience = 100;
    [SerializeField]
    private int Level = 1;
    [SerializeField]
    public int AbilityPoints = 0;

    [SerializeField]
    private Mob_Exp MobExperience;

    void Start()
    {
        GainExperience();
    }

    public void GainExperience()
    {
        Experience = Experience + MobExperience.GiveExperience;
        if (Experience >= MaxExperience)
        {
            MaxExperience  = MaxExperience * 2;
            Level += 1;
            AbilityPoints += 1;
        }
    }

}
