﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransit : MonoBehaviour {

    public void OnClicked(string MapName)
    {
		StartCoroutine(SceneTransitManager.instance.ChangeScene(MapName));
    }
}
