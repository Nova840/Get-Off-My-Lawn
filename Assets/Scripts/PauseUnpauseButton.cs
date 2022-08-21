using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUnpauseButton : MonoBehaviour {

    public void OnButtonClick() {
        PauseGame.Paused = false;
    }

}