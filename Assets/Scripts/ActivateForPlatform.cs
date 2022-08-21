using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateForPlatform : MonoBehaviour {

    [Header("Active on Desktop and Inactive on Mobile, or Vice Versa.")]
    [SerializeField]
    private bool desktop = true;

    private void Start() {
        if (!Application.isMobilePlatform)
            gameObject.SetActive(desktop);
        else
            gameObject.SetActive(!desktop);
    }

}