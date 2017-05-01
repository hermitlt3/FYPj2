using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    private double updateInterval = 0.5;
    private double accum = 0.0; // FPS accumulated over the interval
    private double timeleft; // Left time for current interval
    private int frames = 0; // Frames drawn over the interval

    // Use this for initialization
    void Start()
    {
        if (!GetComponent<GUIText>())
        {
            print("FramesPerSecond needs a GUIText component!");
            enabled = false;
            return;
        }
        timeleft = updateInterval;
    }

    // Update is called once per frame
    void Update()
    {
        timeleft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        ++frames;

        // Interval ended - update GUI text and start new interval
        if (timeleft <= 0.0)
        {
            // display two fractional digits (f2 format)
            GetComponent<GUIText>().text = "FPS - " + (accum / frames).ToString("f2");
            timeleft = updateInterval;
            accum = 0.0;
            frames = 0;
        }
    }
}
