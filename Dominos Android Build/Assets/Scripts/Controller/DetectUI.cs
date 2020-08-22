using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectUI : MonoBehaviour {

    public CanvasScaler canvas;

    void Awake()
    {
        canvas.referenceResolution = new Vector2(Screen.width, Screen.width / 9 * 16);
    }
}
