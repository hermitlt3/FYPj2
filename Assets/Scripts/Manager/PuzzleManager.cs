using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour {

	public static PuzzleManager instance;
	private List<InteractiveObject> puzzleList = new List<InteractiveObject>();

	void Awake () {
		if (instance) {
			Destroy (instance);
			return;
		}
		instance = this;
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AddToPuzzleList(InteractiveObject io) {
		puzzleList.Add (io);
	}

	public void Reset() {
		foreach (InteractiveObject io in puzzleList) {
			if (io.gameObject.activeInHierarchy) {
				io.Reset ();
			}
		}
	}

	public void UpdateKey(InteractiveKey key) {
		foreach (InteractiveObject io in puzzleList) {
			if (io.GetKey () == key) {
				io.GetLock ().SetTrigger (true);
				if (io.GetKey ().DoesItHappenOnce ()) {
					io.GetLock ().WillItHappenOnce (true);
					puzzleList.Remove (io);
					return;
				}
			}
		}
	}
}
