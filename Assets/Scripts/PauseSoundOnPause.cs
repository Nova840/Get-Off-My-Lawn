using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PauseSoundOnPause : MonoBehaviour {

    private AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    private bool wasPaused = false;
    private void Update() {
        if (PauseGame.Paused && !wasPaused)//just paused
            audioSource.Pause();
        else if (!PauseGame.Paused && wasPaused)//just unpaused
            audioSource.UnPause();

        wasPaused = PauseGame.Paused;
    }

}