using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using TMPro;

public class FPSCounterTMP : MonoBehaviour
{
    public TextMeshProUGUI fpsText; // Reference to the TextMeshProUGUI component

    private float deltaTime = 0.0f;

    void Update()
    {
        // Calculate the time between frames
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

        // Calculate FPS
        float fps = 1.0f / deltaTime;

        // Update the TextMeshProUGUI component
        fpsText.text = $"FPS: {fps:0.}";
    }
}
