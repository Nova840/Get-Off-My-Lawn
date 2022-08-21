using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class Player1UsesKeyboard : MonoBehaviour {

    private Toggle toggle;

    public static bool On { get; private set; } = true;

    private void Awake() {
        toggle = GetComponent<Toggle>();
    }

    private void Start() {
        toggle.isOn = On;
    }

    public void OnValueChanged(bool newValue) {
        On = newValue;
    }

}