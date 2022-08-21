using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {

    public static Camera Instance { get; private set; }

    private void Awake() {
        Instance = GetComponent<Camera>();
    }

}