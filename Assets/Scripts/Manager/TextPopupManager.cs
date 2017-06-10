using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPopupManager {

	public enum TEXT_TYPE {
		HEAL = 0,
		DAMAGE,
		TOTAL_TEXTTYPE
	}

	public static void ShowTextPopup(Canvas canvas, Vector3 worldPosition, string value, TEXT_TYPE type) {
		
		GameObject textOutputText = GameObject.Instantiate(Resources.Load("DamagePopupText")) as GameObject;

		TextPopupScript dpScript = textOutputText.GetComponent<TextPopupScript> ();
		dpScript.transform.SetParent (canvas.transform);
		dpScript.transform.position = Camera.main.WorldToScreenPoint (worldPosition);

		switch (type) {
		case TEXT_TYPE.HEAL:

			dpScript.SetText (value, Color.green);
			dpScript.SetOutline (Color.yellow);

			break;
		case TEXT_TYPE.DAMAGE: 

			dpScript.SetText (value, Color.red);
			dpScript.SetOutline (Color.yellow);

			break;
		}
	}

	public static void ShowTextPopup(Canvas canvas, Vector3 worldPosition, string value, Color textColor, Color textOutline) {

		GameObject textOutputText = GameObject.Instantiate (Resources.Load ("DamagePopupText")) as GameObject;

		TextPopupScript dpScript = textOutputText.GetComponent<TextPopupScript> ();
		dpScript.transform.SetParent (canvas.transform);
		dpScript.transform.position = Camera.main.WorldToScreenPoint (worldPosition);
		dpScript.SetText (value, textColor);
		dpScript.SetOutline (textOutline);
	}
}
