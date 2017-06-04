using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadCheckpointSystem {

	private static List<GameObject> enemiesKilled = new List<GameObject>();

	public static void AddEnemyToReloadList(GameObject enemy) {
		enemiesKilled.Add (enemy);
	}

	public static void ReloadAll() {
		foreach (GameObject go in enemiesKilled) {
			go.AddComponent<ResetEnemy> ();
			go.SetActive (true);
		}
		enemiesKilled.Clear ();
	}
}
