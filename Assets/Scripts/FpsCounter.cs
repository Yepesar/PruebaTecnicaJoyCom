using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FpsCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fpsTexts;

    int currentFPS;

    void Update()
    {
        currentFPS = (int)(1f / Time.unscaledDeltaTime);
        
    }

    private void LateUpdate()
    {
        fpsTexts.text = "FPS: " + currentFPS;
    }
}
