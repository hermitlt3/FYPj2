using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aufseer_Attack0 : Boss_Attack {

	private int attackPatternIndex = 0;
	private Aufseer_Pool pool;

	[SerializeField]
	private Vector2 velocitySet = new Vector2(2f, 15f);
	// Use this for initialization
	void Start () {
		// In case the target isnt set properly, then we manually do tis
		if (target == null) {
			target = GameObject.FindGameObjectWithTag ("Player");
		}
		pool = GetComponent<Aufseer_Pool> ();
		doAction ();
	}

	// Update is called once per frame
	void Update () {
		
	}

	public override void doAction() {
		for (int i = 0; i < 5; ++i) {
			GameObject temp = pool.GetPooledObject (attackPatternIndex);
			temp.transform.parent = transform;
			temp.transform.position = new Vector3(transform.position.x, transform.position.y + 0.8f, transform.position.z + 0.1f);
			temp.SetActive (true);
            temp.GetComponent<AudioSource>().Play();

			int targetDir = (target.transform.position.x < transform.position.x) ? 1 : -1;
			temp.AddComponent<Aufseer_Attack0Behavior> ().SetTarget(target);

			temp.GetComponent<Rigidbody2D>().velocity = new Vector2 (targetDir * velocitySet.x * i, velocitySet.y);

		}
		Destroy (this);
	}
}
