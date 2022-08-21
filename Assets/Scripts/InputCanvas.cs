using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCanvas : MonoBehaviour {

    public static RectTransform Stick { get; private set; }
    public static RectTransform StickBackground { get; private set; }

    [SerializeField]
    private RectTransform stick = null, stickBackground = null;

    private void Awake() {
        Stick = stick;
        StickBackground = stickBackground;
    }

}