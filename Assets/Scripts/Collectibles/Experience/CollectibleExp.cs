using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleExp : CollectibleBase {

	private Mob_Exp expScript;
	private float expAmount;

	private int numberOfExpSprites;

	// Exp divides by divideNum = numberOfExpSprites
	[SerializeField]
	private int divideNum = 2;

	// Use this for initialization
	protected override void Start () {
		base.Start ();

		expScript = GetComponent<Mob_Exp> ();
		if (expScript == null) {
			Debug.Log ("No exp script");
			return;
		}
		expAmount = expScript.GiveExperience;
		//numberOfExpSprites = expAmount / divideNum + expAmount % divideNum;
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
	}
}
