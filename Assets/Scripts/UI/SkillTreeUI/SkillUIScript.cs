using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillUIScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	public GameObject description;

	public int flipX;
	public int flipY;

	private RectTransform rectTransform;
	// Use this for initialization
	void Start () {
		rectTransform = GetComponent<RectTransform> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnPointerEnter(PointerEventData eventData) {
		description.GetComponent<SkillDescriptionScript> ().ShowDescription (rectTransform, flipX, flipY);
	}

	public void OnPointerExit(PointerEventData eventData) {
		description.GetComponent<SkillDescriptionScript> ().HideDescription ();
	}
}
