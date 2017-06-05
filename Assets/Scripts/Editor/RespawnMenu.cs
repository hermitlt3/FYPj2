using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class RespawnMenu {

	[MenuItem ("Respawn/Respawn Enemies")]
	static void RespawnEnemies() {
		if (Selection.activeObject == null) {
			Debug.Log ("No respawn selected.");
		} else {
			ReloadCheckpointSystem.ReloadAll ();
		}
	}
}
