using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenuItem : MonoBehaviour {

    [SerializeField]
    private Button button = null;

    [SerializeField]
    private Toggle toggle = null;

    [SerializeField]
    private Button upButton = null, downButton = null;

    [SerializeField]
    private Slider slider = null;

    public bool Submit() {
        if (button)
            button.onClick.Invoke();
        if (toggle)
            toggle.isOn = !toggle.isOn;
        return CanSubmit();
    }

    public bool CanSubmit() {
        return button || toggle;
    }

    public bool Add() {
        if (upButton)
            upButton.onClick.Invoke();
        if (slider)
            slider.value++;
        return CanAdd();
    }

    public bool CanAdd() {
        return upButton || slider;
    }

    public bool Subtract() {
        if (downButton)
            downButton.onClick.Invoke();
        if (slider)
            slider.value--;
        return CanSubtract();
    }

    public bool CanSubtract() {
        return downButton || slider;

    }

}