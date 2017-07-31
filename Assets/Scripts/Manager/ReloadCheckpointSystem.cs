using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadCheckpointSystem {

	public static List<GameObject> enemiesKilled = new List<GameObject>();

	public static void AddEnemyToReloadList(GameObject enemy) {
		enemiesKilled.Add (enemy);
	}

	public static void RemoveEnemyToReloadList(GameObject enemy) {
		enemiesKilled.Remove (enemy);
	}

	public static void ReloadAll() {
		if (enemiesKilled.Count <= 0) {
			return;
		}

		foreach (GameObject go in enemiesKilled) {
			go.AddComponent<ResetEnemy> ();
			go.SetActive (true);
		}
		enemiesKilled.Clear ();
	}

    public static void DestroyStageEnemies()
    {
        enemiesKilled.Clear();
    }
}
