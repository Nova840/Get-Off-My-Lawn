using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicVolume : MonoBehaviour {

    public static Volume Instance { get; private set; }

    private void Awake() {
        Instance = GetComponent<Volume>();
    }

}