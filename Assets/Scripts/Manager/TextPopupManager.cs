using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextPopupManager : MonoBehaviour {

	public static TextPopupManager instance;
	public GameObject[] prefabs;
	public enum TEXT_TYPE {
		HEAL = 0,
		DAMAGE,
		CRITICAL,
		TOTAL_TEXTTYPE
	}

	void Awake() {
		if (instance && instance != this) {
			Destroy (instance);
			return;
		}
		instance = this;
	}

	void Start() {
		if (prefabs.Length < (int)TEXT_TYPE.TOTAL_TEXTTYPE) {
			for (int i = (int)TEXT_TYPE.TOTAL_TEXTTYPE; i > prefabs.Length; --i) {
				prefabs [i] = prefabs [prefabs.Length - 1];
			}
		}
	}

	public void ShowTextPopup(Canvas canvas, Vector3 worldPosition, string value, TEXT_TYPE type) {

		if (canvas == null) {
			canvas = GameObject.FindGameObjectWithTag ("PlayerCanvas").GetComponent<Canvas> ();
		}
		GameObject go = GameObject.Instantiate (prefabs [(int)type]);
		Vector3 randomness = new Vector3(Random.Range(-1f, 1f), Random.Range(1f, 1f));
        Vector3 screenPos = Camera.main.WorldToScreenPoint( new Vector3(worldPosition.x + randomness.x, worldPosition.y - randomness.y));

        // This to ensure the canvas scale properly
        go.transform.SetParent (canvas.transform, false);

        go.GetComponent<RectTransform>().position = screenPos;
        go.GetComponent<Text> ().text = value;
	}

	public void ShowDamageTextPopup(Canvas canvas, Vector3 worldPosition, int value) {

		if (canvas == null) {
			canvas = GameObject.FindGameObjectWithTag ("PlayerCanvas").GetComponent<Canvas> ();
		}
		GameObject go = GameObject.Instantiate (prefabs [(int)TEXT_TYPE.DAMAGE]);
		go.transform.SetParent (canvas.transform, false);
		Vector3 randomness = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
		go.transform.position = Camera.main.WorldToScreenPoint (worldPosition + randomness);
		go.GetComponent<Text> ().text = "-" + value.ToString();
	}
}
