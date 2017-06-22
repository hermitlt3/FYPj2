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
		
		GameObject go = GameObject.Instantiate (prefabs [(int)type]);
		go.transform.SetParent (canvas.transform, false);
		go.transform.position = Camera.main.WorldToScreenPoint (worldPosition);
		go.GetComponent<Text> ().text = value;
	}
}
