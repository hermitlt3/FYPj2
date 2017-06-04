using UnityEngine;
using System.Collections;

public class Skill_CritStat : MonoBehaviour {

    [SerializeField]
    private float CriticalStat = 0.0f;

    public float SkillTimer = 30.0f;

    void Update()
    {
        if (SkillTimer > 0.0f)
        {
            CriticalStat = 100.0f;
            SkillTimer = Mathf.Max(0.0f, SkillTimer - Time.deltaTime);
        }
        else if (SkillTimer <= 0.0f)
        {
            CriticalStat = 0.0f;
            Destroy(this);
        }

    }
}
