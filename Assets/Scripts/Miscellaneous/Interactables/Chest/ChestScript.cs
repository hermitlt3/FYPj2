using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour {

	public enum DROPPEDITEM_TYPE {
		EXPERIENCE = 0,
		HEALTH,
		BOTH,
		NOTHING
	} 
	public DROPPEDITEM_TYPE itemType = DROPPEDITEM_TYPE.EXPERIENCE;
	public int healthValue;
	public int expValue;

	Stat_HealthScript health;
	Animator thisAnimator;

    bool playSound;

	// Use this for initialization
	void Start () {
		health = GetComponent<Stat_HealthScript> ();
		thisAnimator = GetComponent<Animator> ();

		healthValue = Mathf.Max (0, healthValue);
		expValue = Mathf.Max (0, expValue);

		if (healthValue == 0 && expValue == 0 && itemType != DROPPEDITEM_TYPE.NOTHING) {
			itemType = DROPPEDITEM_TYPE.NOTHING;
		} else if (healthValue == 0 && expValue != 0 && itemType != DROPPEDITEM_TYPE.EXPERIENCE) {
			itemType = DROPPEDITEM_TYPE.EXPERIENCE;
		} else if (healthValue != 0 && expValue == 0 && itemType != DROPPEDITEM_TYPE.HEALTH) {
			itemType = DROPPEDITEM_TYPE.HEALTH;
		} else if (healthValue != 0 && expValue != 0 && itemType != DROPPEDITEM_TYPE.BOTH) {
			itemType = DROPPEDITEM_TYPE.BOTH;
		}

        playSound = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (!health.isAlive ()) {
			thisAnimator.SetBool ("isOpened", true);
            if(playSound)
            {
                AudioManager.instance.PlaySound(GetComponent<AudioSource>());
                playSound = false;
            }
		}
	}

	void DoAction () {
		switch (itemType) {
		case DROPPEDITEM_TYPE.EXPERIENCE: 
			CollectiblesGenerator.instance.GenerateEXP (transform.position, expValue);
			break;
		case DROPPEDITEM_TYPE.HEALTH:
			CollectiblesGenerator.instance.GenerateHealth (transform.position, healthValue);
			break;
		case DROPPEDITEM_TYPE.BOTH:
			CollectiblesGenerator.instance.GenerateCollectibles (transform.position, expValue, healthValue);
			break;
		}
	}
}
