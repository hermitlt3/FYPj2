using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aufseer_Attack1 : Boss_Attack {

	private BoxCollider2D arenaBounds;
	private Aufseer_Pool pool;
	private int attackPatternIndex = 1;

	// Use this for initialization
	void Start () {
		arenaBounds = GameObject.FindGameObjectWithTag ("Boss Arenas").GetComponent<BoxCollider2D> ();
		pool = GetComponent<Aufseer_Pool> ();
		doAction ();
	}

	// Update is called once per frame
	void Update () {

	}

	public override void doAction() {
		GameObject fireBall = pool.GetPooledObject (attackPatternIndex);
		float positionX = (target.transform.position.x < transform.position.x) ? arenaBounds.bounds.max.x : arenaBounds.bounds.min.x;

		fireBall.AddComponent<Aufseer_Attack1Behavior> ().direction = (target.transform.position.x < transform.position.x) ? -1 : 1;
		fireBall.transform.parent = transform;
		fireBall.transform.position = new Vector3 (positionX , transform.position.y + 1f, transform.position.z + 0.1f);
		fireBall.SetActive (true);

		Destroy (this);
	}
}
