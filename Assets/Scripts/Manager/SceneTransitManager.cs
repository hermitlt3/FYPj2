﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitManager : MonoBehaviour {

	public static SceneTransitManager instance;

	public TransitFade fading;

	void Awake() {
		if (instance && instance != this) {
			Destroy (instance);
			return;
		}
		instance = this;
		DontDestroyOnLoad (this.gameObject);
	}

	// Use this for initialization
	void Start () {
		fading = transform.GetComponentInChildren<TransitFade>();	
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OnEnable()
	{
		//Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
		SceneManager.sceneLoaded += OnLevelFinishedLoading;
	}

	void OnDisable()
	{
		//Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
		SceneManager.sceneLoaded -= OnLevelFinishedLoading;
	}

	void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
	{
		fading.BeginFade (-1);
	}

	public IEnumerator ReloadSceneWithFade(int dir ) {
		if (Mathf.Clamp (dir, -1, 1) == 1) {
			float fadeTime = fading.BeginFade (1);
			yield return new WaitForSeconds (fadeTime);
		} else {
			float fadeTime = fading.BeginFade (-1);
			yield return new WaitForSeconds (fadeTime);
		}
	}

	public IEnumerator ChangeScene(string sceneName) 
	{
		float fadeTime = fading.BeginFade (1);
		yield return new WaitForSeconds (fadeTime);

		AsyncOperation async = SceneManager.LoadSceneAsync (sceneName);
		yield return async;
	}
}
