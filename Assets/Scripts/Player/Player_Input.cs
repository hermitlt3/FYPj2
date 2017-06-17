﻿using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Player))]
public class Player_Input : MonoBehaviour {

	Player player;

	void Start () {
		player = GetComponent<Player> ();
	}

	void Update () {
		Vector2 directionalInput = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		player.SetDirectionalInput (directionalInput);

		if (Input.GetKey (KeyCode.Space)) {
			player.OnJumpInputDown ();
		}
		if (Input.GetButton ("Fire1")) {
			player.OnInteraction ();
		}

	}
}
