using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnpauseGameOnAwake : MonoBehaviour {

    private void Awake() {
        PauseGame.Paused = false;
    }

}