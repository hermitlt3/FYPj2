using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitManager : MonoBehaviour {

	public static SceneTransitManager instance;
	public enum TRANSIT_TYPE
	{
		FADE
	}
	public TransitFade fading;

	void Awake() {
		if (instance && instance != this) {
			Destroy (instance);
			return;
		}
		instance = this;
	}

	// Use this for initialization
	void Start () {
		fading = transform.GetComponentInChildren<TransitFade>();	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public IEnumerator ReloadScene(TRANSIT_TYPE type) {
		switch (type) {
		case TRANSIT_TYPE.FADE:
			yield return new WaitForSeconds (fading.BeginFade (1));
			break;
		}
	}

	public IEnumerator TransitScene(TRANSIT_TYPE type) {
		switch (type) {
		case TRANSIT_TYPE.FADE:
			break;
		}
		yield return new  WaitForSeconds(0.4f);
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

	}
}
