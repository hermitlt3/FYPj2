using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TerrainMaker : EditorWindow {

	static int count = 0;
	int customCount = -1;
	int xSize;
	int ySize;
	int sortingOrder = 0;

	bool doShit;
	bool createCollider;
	bool isThrough;
	bool addKineticRigidbody;

	float scale = 1f;
	const float spriteSize = 5f;

	Sprite[] sprites = new Sprite[9];
	Sprite throughSprite;

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

	// For collider
	Vector2 offset = new Vector2(0.5f, 2f);
	List<Vector2> pathList = new List<Vector2>();
	Vector2[] thePath;

	[MenuItem ("Game stuff/Make Terrain")]
	static void MakeTerrain() {
		TerrainMaker window = (TerrainMaker)EditorWindow.GetWindow(typeof(TerrainMaker), false, "Terrain Maker");
		window.Show();
	}

	void OnGUI() {
		OnGUITerrainSettings ();

	}

	void Update() {
		CreateTerrain ();
	}

	//Monster
	void OnGUITerrainSettings() {
		GUILayout.Label("IMPORTANT: DON'T ROTATE THE THROUGH TERRAINS! IT WILL CAUSE A SHIT TON OF BUGS", EditorStyles.boldLabel);
		customCount = EditorGUILayout.IntField("Count name", customCount, GUILayout.Width(300));
		if (customCount != -1)
			count = customCount;

		GUILayout.Label("Terrain Size", EditorStyles.boldLabel);

		GUILayout.Label("X Size", GUILayout.Width(50));
		xSize = EditorGUILayout.IntSlider(xSize, 0, 10, GUILayout.Width(300));
		GUILayout.Label("Y Size", GUILayout.Width(50));
		ySize = EditorGUILayout.IntSlider(ySize, 0, 10, GUILayout.Width(300));


		GUILayout.Label("Terrain Scale", EditorStyles.boldLabel);
		scale = EditorGUILayout.Slider (scale, 0.1f, 10f, GUILayout.Width(300));

		sortingOrder = EditorGUILayout.IntField("Sorting order", sortingOrder, GUILayout.Width(300));

		resourceType = (GETRESOURCESTYPE)EditorGUILayout.EnumPopup (resourceType, GUILayout.Width(300));
		SpriteSorter ();

		createCollider = EditorGUILayout.Toggle ("Create collider", createCollider, GUILayout.Width (300));
		if (createCollider) {
			offset = EditorGUILayout.Vector2Field ("Offset", offset, GUILayout.Width (300));
		}
		isThrough = EditorGUILayout.Toggle ("Is through", isThrough, GUILayout.Width (300));
		addKineticRigidbody = EditorGUILayout.Toggle ("Add kinematic body", addKineticRigidbody, GUILayout.Width (300));

		doShit = GUILayout.Button ("Make Terrain", GUILayout.Width(300));
	}

	void CreateTerrain() {
		if (!doShit) {
			return;
		}
		GameObject parent = new GameObject ("TerrainNo"+ count++);
		customCount = count;
		parent.transform.gameObject.layer = LayerMask.NameToLayer ("Terrain");

		for (int i = 0; i <= xSize + 1; ++i) {
			for (int j = 0; j <= ySize + 1; ++j) {
				GameObject go = new GameObject ("Terrain" + i + j);
				go.transform.SetParent(parent.transform);

				SpriteArranger (go, i, j);
				SpritePositioner (go, i, j);
				go.GetComponent<SpriteRenderer> ().sortingOrder = sortingOrder;
				if (isThrough) {
					go.GetComponent<SpriteRenderer> ().sortingLayerName = "Behind player";
				} 

			}
		}
		if (createCollider) {
			PolygonCollider2D collider = parent.AddComponent<PolygonCollider2D> ();
			collider.SetPath(0, CreateCollider (parent, offset.x, offset.y));
		}
		if (addKineticRigidbody) {
			parent.AddComponent<Rigidbody2D> ().isKinematic = true;
		}
		if (isThrough) {
			parent.transform.gameObject.tag = "Through";
			parent.AddComponent<ThroughTerrainScript> ();
			parent.GetComponent<PolygonCollider2D> ().isTrigger = true;
		}
		doShit = false;
	}

	void SpriteSorter() {
		switch (resourceType) {
		case GETRESOURCESTYPE.ENUM_SPRITE:
			type = (TYPE)EditorGUILayout.EnumPopup (type,GUILayout.Width(300));
			switch (type) {
			case TYPE.DAY:
				for (int i = 0; i < sprites.Length; ++i) {
					filePath = "Brutal 2D Adventure Jungle Pack/day/platform-blocks/platform-block-0";
					sprites [i] = Resources.Load <Sprite> (filePath + (i + 1));
				}
				if (isThrough) {
					if (xSize > 0) {
						throughSprite = Resources.Load<Sprite> (filePath + (sprites.Length + 1));
					} else {
						throughSprite = Resources.Load<Sprite> (filePath + (sprites.Length + 2));
					}
				}
				break;
			case TYPE.NIGHT:
				for (int i = 0; i < sprites.Length; ++i) {
					filePath = "Brutal 2D Adventure Jungle Pack/night/platform-blocks/platform-block-0";
					sprites [i] = Resources.Load <Sprite> (filePath + (i + 1));
				}
				if (isThrough) {
					if (xSize > 0) {
						throughSprite = Resources.Load<Sprite> (filePath + (sprites.Length + 1));
					} else {
						throughSprite = Resources.Load<Sprite> (filePath + (sprites.Length + 2));
					}
				}
				break;
			}
			break;
		case GETRESOURCESTYPE.CUSTOM:
			scrollPos = EditorGUILayout.BeginScrollView (scrollPos, GUILayout.Height (300), GUILayout.Width (300));
			string temp = "";
			for (int i = 0; i < sprites.Length; ++i) {
				switch (i) {
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
				sprites [i] = (Sprite)EditorGUILayout.ObjectField (temp, sprites [i], typeof(Sprite), false);
			}
			if (isThrough) {
				throughSprite = (Sprite)EditorGUILayout.ObjectField (temp, throughSprite, typeof(Sprite), false);
			}
			EditorGUILayout.EndScrollView ();
			break;
		case GETRESOURCESTYPE.RESOURCE_NAME:
			GUILayout.Label ("File path should be the sprite name until the changing num", EditorStyles.boldLabel);

			filePath = EditorGUILayout.TextField ("File path = Resources/", filePath, GUILayout.Width (500));
			for (int i = 0; i < sprites.Length; ++i) {
				sprites [i] = Resources.Load <Sprite> (filePath + (i + 1));
			}
			if (isThrough) {
				if (xSize > 0) {
					throughSprite = Resources.Load<Sprite> (filePath + (sprites.Length + 1));
				} else {
					throughSprite = Resources.Load<Sprite> (filePath + (sprites.Length + 2));
				}
			}
			break;
		}
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
		go.transform.localPosition = new Vector3 (finalX, finalY); 	
		go.GetComponent<SpriteRenderer> ().sortingLayerID = SortingLayer.NameToID ("In front player");
	}

	Vector2[] CreateCollider(GameObject parent, float offsetX = 0.5f, float offsetY = 2f) {
		Vector2 minPoint = new Vector2(-0.5f, -0.5f);
		Vector2 maxPoint = new Vector2(0.5f, 0.5f);

		pathList.Clear ();
		foreach (Transform child in parent.transform) {
			Bounds childBounds = child.gameObject.GetComponent<SpriteRenderer> ().bounds;
			minPoint.x = Mathf.Min (minPoint.x, childBounds.min.x);
			maxPoint.x = Mathf.Max (maxPoint.x, childBounds.max.x);
			minPoint.y = Mathf.Min (minPoint.y, childBounds.min.y);
			maxPoint.y = Mathf.Max (maxPoint.y, childBounds.max.y);
		}
		offsetX *= scale;
		offsetY *= scale;

		Vector2 point;
		point = new Vector2 (minPoint.x + offsetX, minPoint.y);	// Min x, min y
		pathList.Add (point);
		point = new Vector2 (minPoint.x + offsetX + 0.1f, maxPoint.y - offsetY);	// Min x, max y
		pathList.Add (point);		  					      
		point = new Vector2 (maxPoint.x - offsetX - 0.1f, maxPoint.y - offsetY);	// Min x, max y 
		pathList.Add (point);		   					      
		point = new Vector2 (maxPoint.x - offsetX, minPoint.y);	// Min x, min y
		pathList.Add (point);

		thePath = new Vector2[pathList.Count];
		int i = 0;
		foreach (Vector2 v in pathList) {
			thePath [i++] = v;
		}
		return thePath;
	}

	void ForFakeTerrain(ref GameObject go)
	{
		go.AddComponent<SpriteRenderer> ().sprite = throughSprite;
		go.GetComponent<SpriteRenderer> ().sortingLayerID = SortingLayer.NameToID ("In front player");
		go.GetComponent<SpriteRenderer> ().sortingOrder = sortingOrder;
		go.transform.localScale = new Vector3 (scale, scale);

		Bounds childBounds = go.GetComponent<SpriteRenderer> ().bounds;
		Vector2 minPoint = new Vector2 (-0.5f, -0.5f);
		Vector2 maxPoint = new Vector2(0.5f, 0.5f);

		minPoint.x = childBounds.min.x;
		maxPoint.x = childBounds.max.x;
		minPoint.y = childBounds.min.y;
		maxPoint.y = childBounds.max.y;

		go.AddComponent<BoxCollider2D> ();
		go.GetComponent<BoxCollider2D> ().size = new Vector2 (go.GetComponent<BoxCollider2D> ().size.x, go.GetComponent<BoxCollider2D> ().size.y * 0.5f);
	}
}
