using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSlider : MonoBehaviour {

    [SerializeField]
    private Slider slider = null;

    private void Start() {
        slider.value = Sound.SoundVolume * slider.maxValue;
    }

    public void OnValueChanged(float newValue) {
        Sound.SoundVolume = newValue / slider.maxValue;
    }

}