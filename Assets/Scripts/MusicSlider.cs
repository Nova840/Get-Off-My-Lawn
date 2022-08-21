using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicSlider : MonoBehaviour {

    [SerializeField]
    private Slider slider = null;

    private void Start() {
        slider.value = Sound.MusicVolume * slider.maxValue;
    }

    public void OnValueChanged(float newValue) {
        Sound.MusicVolume = newValue / slider.maxValue;
    }

}