using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateOnButtonPress : MonoBehaviour {

    private void Update() {
        if (Input.GetButtonDown("All Joysticks Button 1") || Input.GetButtonDown("K Escape") || GetTouchUp())
            gameObject.SetActive(false);
    }

    private bool GetTouchUp() {
        foreach (Touch t in Input.touches)
            if (t.phase == TouchPhase.Ended)
                return true;
        return false;
    }

}