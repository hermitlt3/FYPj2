using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitManager : MonoBehaviour {

	public static SceneTransitManager instance;
    public TransitFade fading;

    private List<GameObject> toBeDestroyed;

	void Awake() {
        toBeDestroyed = new List<GameObject>();
        if (instance && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
		DontDestroyOnLoad (this);
	}

	// Use this for initialization
	void Start () {
		fading = transform.GetComponentInChildren<TransitFade>();
	}
	
	// Update is called once per frame
	void Update () {
		
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

	public IEnumerator ChangeScene(string sceneName, float moreTime = 0f) 
	{
		float fadeTime = fading.BeginFade (1);
		yield return new WaitForSeconds (fadeTime);

        yield return new WaitForSeconds(moreTime);

        AsyncOperation async = SceneManager.LoadSceneAsync (sceneName);
		yield return async;
	}

    public IEnumerator FadeInAndOut(float delayTime = 0f, bool instantFadeIn = false, bool instantFadeOut = false)
    {
        if (!instantFadeIn)
        {
            float fadeTime = fading.BeginFade(1);
            yield return new WaitForSeconds(fadeTime);
            yield return new WaitForSeconds(delayTime);
        }
        else
        {
            fading.InstantTransmission(1);
        }

        if(!instantFadeOut)
        {
            float fadeTime = fading.BeginFade(-1);
            yield return new WaitForSeconds(fadeTime);
        }
        else
        {
            fading.InstantTransmission(-1);
        }
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
        print(toBeDestroyed.Count);
        if (toBeDestroyed.Count > 0)
        {
            foreach (GameObject go in toBeDestroyed)
            {
                Destroy(go);
            }
        }
        toBeDestroyed.Clear();
        fading.BeginFade(-1);
    }

    public void AddGameObjectToDestroy(GameObject go)
    {
        toBeDestroyed.Add(go);
    }
}
