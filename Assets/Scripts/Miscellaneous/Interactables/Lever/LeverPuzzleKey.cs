using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverPuzzleKey : InteractiveKey {

	private bool reverse;
	protected override void Start () {
		base.Start ();
		reverse = false;
	}

	// Update is called once per frame
	protected override void Update () {
		base.Update ();
		if (isTriggered) {
            AudioManager.instance.PlaySound(GetComponent<AudioSource>());
			if (!reverse) {
				this.gameObject.GetComponent<SpriteRenderer> ().flipX = true;
				reverse = true;
			} else {
				this.gameObject.GetComponent<SpriteRenderer> ().flipX = false;
				reverse = false;
			}
			isDone = true;
			toUnlock.InitVariables ();
			Reset ();
		}
		if (isDone) {
			healthScript.SetCurrentHealth (1);
		}
		if (toUnlock.isDone) {
			isDone = false;
			toUnlock.isDone = false;
		}
	}

	public override void Reset() {
		base.Reset ();
	}
}
