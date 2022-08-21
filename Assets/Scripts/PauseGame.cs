using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour {

    [SerializeField]
    private GameObject pausedImage = null;

    private static bool paused = false;
    public static bool Paused {
        get {
            return paused;
        }
        set {
            paused = value;
            Time.timeScale = paused ? 0 : 1;
        }
    }

    private void Update() {
        if (EndGame.Ended)
            return;

        if (Input.GetButtonDown("All Joysticks Start") || Input.GetButtonDown("K Escape"))
            Paused = !Paused;

        if (pausedImage.activeSelf != Paused)
            pausedImage.SetActive(Paused);
    }

}