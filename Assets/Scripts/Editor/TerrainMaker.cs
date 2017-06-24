using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TerrainMaker : EditorWindow {

	static int count = 0;
	int xSize;
	int ySize;
	bool doShit;

	float scale = 1f;
	const float spriteSize = 5f;

	Sprite[] sprites = new Sprite[9];
	Vector3 lePosition;

	// For scroll 
	Vector2 scrollPos;

	// For getting resources
	enum TYPE {
		DAY,
		NIGHT
	}
	enum GETRESOURCESTYPE {
		RESOURCE_NAME,
		ENUM_SPRITE,
		CUSTOM
	}
	enum SIDE {
		TOPLEFT = 0,
		TOP,
		TOPRIGHT,
		LEFT,
		MIDDLE,
		RIGHT,
		BTMLEFT,
		BTM,
		BTMRIGHT
	}

	TYPE type;
	GETRESOURCESTYPE resourceType;
	string filePath;

	[MenuItem ("Terrain/Make Terrain")]
	static void MakeTerrain() {
		TerrainMaker window = (TerrainMaker)EditorWindow.GetWindow(typeof(TerrainMaker), false, "Terrain Maker");
		window.Show();
	}

	[MenuItem ("Terrain/Reset Terrain Count")]
	static void ResetTerrainCount() {
		count = 0;
	}

	void OnGUI() {
		OnGUITerrainSettings ();

	}

	void Update() {
		CreateTerrain ();
	}

	//Monster
	void OnGUITerrainSettings() {
		GUILayout.Label("Terrain Size", EditorStyles.boldLabel);

		GUILayout.BeginHorizontal ();

		GUILayout.Label("X Size", EditorStyles.boldLabel);
		xSize = EditorGUILayout.IntSlider(xSize, 0, 10);
		GUILayout.Label("Y Size", EditorStyles.boldLabel);
		ySize = EditorGUILayout.IntSlider(ySize, 0, 10);

		GUILayout.EndHorizontal ();
		GUILayout.BeginHorizontal ();

		GUILayout.Label("Scale", EditorStyles.boldLabel);
		scale = EditorGUILayout.Slider (scale, 1f, 10f);

		GUILayout.EndHorizontal ();
		GUILayout.BeginHorizontal ();

		lePosition = EditorGUILayout.Vector3Field ("Position", lePosition);

		GUILayout.EndHorizontal ();
		resourceType = (GETRESOURCESTYPE)EditorGUILayout.EnumPopup (resourceType);

		switch (resourceType) {
		case GETRESOURCESTYPE.ENUM_SPRITE:
			type = (TYPE)EditorGUILayout.EnumPopup (type);
			switch (type) {
			case TYPE.DAY:
				for (int i = 0; i < sprites.Length; ++i) {
					filePath = "Brutal 2D Adventure Jungle Pack/day/platform-blocks/platform-block-0";
					sprites [i] = Resources.Load <Sprite>(filePath + (i+1));
				}
				break;
			case TYPE.NIGHT:
				for (int i = 0; i < sprites.Length; ++i) {
					filePath = "Brutal 2D Adventure Jungle Pack/night/platform-blocks/platform-block-0";
					sprites [i] = Resources.Load <Sprite>(filePath + (i+1));
				}
				break;
			}
			break;
		case GETRESOURCESTYPE.CUSTOM:
			scrollPos = EditorGUILayout.BeginScrollView (scrollPos, GUILayout.Height(300));
			string temp = "";
			for (int i = 0; i < sprites.Length; ++i) {
				switch(i) {
				case 0:
					temp = "Top left image";
					break;
				case 1:
					temp = "Top image";
					break;
				case 2: 
					temp = "Top right image";
					break;
				case 3:
					temp = "Left image";
					break;
				case 4:
					temp = "Middle image";
					break;
				case 5:
					temp = "Right image";
					break;
				case 6:
					temp = "Bottom left image";
					break;
				case 7:
					temp = "Bottom image";
					break;
				case 8:
					temp = "Bottom right image";
					break;
				}
				sprites[i] = (Sprite)EditorGUILayout.ObjectField (temp, sprites[i], typeof(Sprite), false);
			}
			EditorGUILayout.EndScrollView ();
			break;
		case GETRESOURCESTYPE.RESOURCE_NAME:
			GUILayout.Label ("File path should be the sprite name until the changing num", EditorStyles.boldLabel);

			GUILayout.BeginHorizontal ();
			GUILayout.Label ("File path = Resources/");
			filePath = EditorGUILayout.TextField (filePath);
			GUILayout.EndHorizontal ();
			for (int i = 0; i < sprites.Length; ++i) {
				sprites [i] = Resources.Load <Sprite>(filePath + (i+1));
			}
			break;
		}

		doShit = GUILayout.Button ("Make Terrain");
	}

	void CreateTerrain() {
		if (!doShit) {
			return;
		}
		GameObject parent = new GameObject ("TerrainNo"+ count++);
		for (int i = 0; i <= xSize + 1; ++i) {
			for (int j = 0; j <= ySize + 1; ++j) {
				GameObject go = new GameObject ("Terrain" + i + j);
				go.transform.SetParent(parent.transform);
				SpriteArranger (go, i, j);
				SpritePositioner (go, i, j);
			}
		}
		doShit = false;
	}

	void SpriteArranger(GameObject go, int i, int j) {
		if (i == 0) {
			if (j == 0) {
				go.AddComponent<SpriteRenderer> ().sprite = sprites [(int)SIDE.TOPLEFT];

			} else if (j == ySize + 1) {
				go.AddComponent<SpriteRenderer> ().sprite = sprites [(int)SIDE.BTMLEFT];
			}
		} else if (i == xSize + 1) {
			if (j == 0) {
				go.AddComponent<SpriteRenderer> ().sprite = sprites [(int)SIDE.TOPRIGHT];

			} else if (j == ySize + 1) {
				go.AddComponent<SpriteRenderer> ().sprite = sprites [(int)SIDE.BTMRIGHT];
			}
		}

		if ((j > 0 && j < ySize + 1) && (i > 0 && i < xSize + 1)) {
			go.AddComponent<SpriteRenderer> ().sprite = sprites [(int)SIDE.MIDDLE];

		} else if (j > 0 && j < ySize + 1) {				
			if (i == 0) {	// First column
				go.AddComponent<SpriteRenderer> ().sprite = sprites [(int)SIDE.LEFT];

			} else if (i == xSize + 1) { // Last column
				go.AddComponent<SpriteRenderer> ().sprite = sprites [(int)SIDE.RIGHT];
			}
		} else if (i > 0 && i < xSize + 1) {
			if (j == 0) {	// First row
				go.AddComponent<SpriteRenderer> ().sprite = sprites [(int)SIDE.TOP];

			} else if (j == ySize + 1) { // Last row
				go.AddComponent<SpriteRenderer> ().sprite = sprites [(int)SIDE.BTM];
			}
		}
	}

	void SpritePositioner(GameObject go, int i, int j) {
		// Normally is 1 * 2.5f
		float finalSpriteSize = scale * spriteSize;

		float startX = (xSize + 1) * -0.5f * finalSpriteSize;		
		float startY = (ySize + 1) * 0.5f * finalSpriteSize;

		// X ++ to move right Y -- to move down
		float finalX = startX + i * finalSpriteSize;
		float finalY = startY - j * finalSpriteSize;

		// Cuz of sprite which tbe sides are halved, so these positions need to be manually changed!
		if (i == 0) { 
			finalX += finalSpriteSize * 0.5f;
		} 
		if (xSize == 0) {
			finalX = 0;
		}
		else if (i == xSize + 1) {
			finalX -= finalSpriteSize * 0.5f;
		}
		if (j == 0) {
			finalY -= finalSpriteSize * 0.5f;
		}
		if (ySize == 0) {
			finalY = 0;
		}
		else if (j == ySize + 1) {
			finalY += finalSpriteSize * 0.5f;
		}
		go.transform.localScale = new Vector3 (scale, scale);
		go.transform.position = lePosition + new Vector3 (finalX, finalY); 	
		go.GetComponent<SpriteRenderer> ().sortingLayerID = SortingLayer.NameToID ("In front player");
	}
}
