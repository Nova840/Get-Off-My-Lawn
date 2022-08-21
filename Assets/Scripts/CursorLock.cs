using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorLock : MonoBehaviour {

    private void Update() {
        if (PauseGame.Paused || EndGame.Ended || SceneManager.GetActiveScene().name != "Game") {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        } else {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
        }
    }

}