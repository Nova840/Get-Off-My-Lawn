using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontActivateIfOtherIsActive : MonoBehaviour {

    [SerializeField]
    private GameObject other = null;

    private void OnEnable() {
        if (other.activeSelf)
            gameObject.SetActive(false);
    }

}