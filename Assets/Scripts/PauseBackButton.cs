using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseBackButton : MonoBehaviour {

    private void Update() {
        if (!EndGame.Ended && PauseGame.Paused && Input.GetButtonDown("All Joysticks Button 1"))//should already be paused but just to be safe I guess
            OnButtonClick();
    }

    public void OnButtonClick() {
        SceneManager.LoadScene("Main Menu");
        //unpauses in start
    }

}
