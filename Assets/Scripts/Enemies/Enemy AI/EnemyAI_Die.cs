using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_Die : MonoBehaviour {

    public int healthGiveMin = 0;
    public int healthGiveMax = 20;
	[SerializeField]
	protected bool youOnlyLiveOnce = false;

	protected bool shouldDie = true;

	protected Stat_ExperienceScript expOutputScript;
	// Use this for initialization
	protected virtual void Start () {
		expOutputScript = GetComponent<Stat_ExperienceScript> ();
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		
	}

	protected virtual void Deactivate() {
		if (!shouldDie) {
			return;
		}
        CollectiblesGenerator.instance.GenerateCollectibles(transform.position, expOutputScript.GetExperience(), Random.Range(healthGiveMin, healthGiveMax));
        if (youOnlyLiveOnce)
        {
            ReloadCheckpointSystem.RemoveEnemyToReloadList(this.gameObject);
        }
        this.gameObject.SetActive(false);
    }

	protected virtual void ShouldDie() {
		if (!GameObject.FindGameObjectWithTag ("Player").GetComponent<Stat_HealthScript> ().isAlive ()) {
			shouldDie = false;
		}
	}

	public void Reset() {
		shouldDie = true;
	}
}
