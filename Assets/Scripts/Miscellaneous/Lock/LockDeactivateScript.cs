using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockDeactivateScript : InteractiveLock {

	public float deactivateTimer = 1f;
	public bool toDestroy = false;

	// Use this for initialization
	protected override void Start () {
		
	}
	
	// Update is called once per frame
	protected override void Update () {
		if (isTriggered) {
            AudioManager.instance.PlaySound(GetComponent<AudioSource>());
			deactivateTimer = Mathf.Max (0, deactivateTimer - Time.deltaTime);
		}
		if (deactivateTimer <= 0) {
            AudioManager.instance.FadeOut(GetComponent<AudioSource>(), 0.5f);
            isDone = true;
			isTriggered = false;

			if (toDestroy) {
				Destroy (this.gameObject);
			} else {
				this.gameObject.SetActive (false);
			}
		}
	}

	public override void InitVariables ()
	{
		isTriggered = true;
	}
}
