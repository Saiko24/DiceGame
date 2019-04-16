using UnityEngine;
using System.Collections;
using SO;

public class FPSDisplay : MonoBehaviour
{
    float deltaTime = 0.0f;
    public Seed seed;
    public StringVariable Platform;

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }

    void OnGUI()
    {
        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(0, 0, w, h * 2 / 100);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 2 / 100;
        style.normal.textColor = new Color32(255, 255, 255, 255);
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.0} ms ({1:0.} fps)   Seed: {2}   Platform: {3}", msec, fps, seed.value, Platform.value);
        GUI.Label(rect, text, style);
    }
}