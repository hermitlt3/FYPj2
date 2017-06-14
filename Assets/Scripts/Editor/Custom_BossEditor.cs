using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Boss_AI))]
public class Custom_BossEditor: Editor {

	private Boss_AI bossAI = null;
	private bool difficulty = false;
	private bool calculatedAttacks = false;

	void OnEnable()
	{
		bossAI = (Boss_AI)target;
	}

	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI ();

		GUILayout.BeginHorizontal ();
		GUILayout.Label ("Player dependent attacks");
		calculatedAttacks = EditorGUILayout.Toggle (calculatedAttacks);
		bossAI.playerDependentAttacks = calculatedAttacks;
		GUILayout.EndHorizontal ();

		GUILayout.BeginHorizontal ();
		GUILayout.Label ("Difficulty curve");
		difficulty = EditorGUILayout.Toggle (difficulty);
		bossAI.incrementDifficulty = difficulty;
		GUILayout.EndHorizontal ();

		if (bossAI.incrementDifficulty) {
			GUILayout.BeginHorizontal ();
			GUILayout.Label ("Hp% drop to increase difficulty");
			GUILayout.EndHorizontal ();

			GUILayout.BeginHorizontal ();
			bossAI.healthPercentageDecrease = EditorGUILayout.IntSlider (bossAI.healthPercentageDecrease, 1, 99, GUILayout.Width(200));
			GUILayout.EndHorizontal ();


			GUILayout.BeginHorizontal ();
			GUILayout.Label ("Rest time lowered per hp% drop");
			GUILayout.EndHorizontal ();

			GUILayout.BeginHorizontal ();
			float maxTime = bossAI.maxTimeBetweenIntervals / (100 / bossAI.healthPercentageDecrease);
			// An offset of 0.1s is added in so that the boss wont be so hax
			bossAI.timeBetweenIntervals = EditorGUILayout.Slider(bossAI.timeBetweenIntervals, 0, maxTime,  GUILayout.Width(200));

			GUILayout.EndHorizontal ();

		}
	}
}
