using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpCollectiblePoolScript : ObjectPoolScript {

	protected override void Awake() {
		DontDestroyOnLoad (this.gameObject);
		instance = this;
	}

	// Use this for initialization
	protected override void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
	}
}
