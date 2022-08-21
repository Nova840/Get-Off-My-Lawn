using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DisableImageIfNoControllersOrOnMobile : MonoBehaviour {

    private Image image;

    private void Awake() {
        image = GetComponent<Image>();
    }

    private void Update() {
        image.enabled = Input.GetJoystickNames().Count(c => c != "") >= 1 && !Application.isMobilePlatform;
    }

}