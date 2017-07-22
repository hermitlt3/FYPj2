using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AddMaterialToSprite : EditorWindow {

	Material material;
	Sprite theSprite;
	bool doShit;
    bool doOffsetShit;

	[MenuItem ("Game stuff/Put Material to Sprite")]
	static void TheChoice() {
		AddMaterialToSprite window = (AddMaterialToSprite)EditorWindow.GetWindow(typeof(AddMaterialToSprite), false, "AddMaterialToSprite");
		window.Show();
	}

	void OnGUI() {
		OnGUITerrainSettings ();

	}

	void Update() {
		if (doShit) {
			DoShit ();
			doShit = false;
		}
        if(doOffsetShit)
        {
            DoOffsetShit();
            doOffsetShit = false;
        }
	}

	//Monster
	void OnGUITerrainSettings() {
		material = (Material)EditorGUILayout.ObjectField ("Material", material, typeof(Material), false);
		theSprite = (Sprite)EditorGUILayout.ObjectField ("The Sprite", theSprite, typeof(Sprite), false);
		doShit = GUILayout.Button ("Apply and WAIT.", GUILayout.Width(300));
        GUILayout.Label("The button below is for offseting");
		doOffsetShit = GUILayout.Button ("Offset", GUILayout.Width(300));
    }

    void DoShit() {
		GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject> ();
		foreach (GameObject hi in allObjects) {
			if (hi.GetComponent<SpriteRenderer> ()) {
				if (hi.GetComponent<SpriteRenderer> ().sprite == theSprite) {
					hi.GetComponent<SpriteRenderer> ().material = material;
				}
				if (hi.transform.localScale.z != 1) {
					hi.transform.localScale = new Vector3 (hi.transform.localScale.x, hi.transform.localScale.y, 1);
				}
			}
		}
	}

    void DoOffsetShit()
    {
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (GameObject hi in allObjects)
        {
            if (hi.GetComponent<SpriteRenderer>())
            {
                if (hi.GetComponent<SpriteRenderer>().sprite == theSprite)
                {
                    hi.transform.position += new Vector3(0.02f, 0);
                }
            }
        }
    }
}
