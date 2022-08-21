using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateWhenGameEnds : MonoBehaviour {

    private void Update() {
        if (EndGame.Ended)
            gameObject.SetActive(false);
    }

}