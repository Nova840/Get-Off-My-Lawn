using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Volume : MonoBehaviour {

    private AudioSource audioSource;

    [SerializeField]
    private float multiplier = 1;

    [SerializeField]
    private float fadeSpeed = 1;

    public float Multiplier { get { return multiplier; } set { multiplier = Mathf.Clamp01(value); } }

    private float targetMultiplier;

    private enum SoundType {
        Sound, Music
    }

    [SerializeField]
    private SoundType type = SoundType.Sound;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
        targetMultiplier = Multiplier;
    }

    public void FadeToVolume(float newVolume) {
        targetMultiplier = newVolume;
    }

    public void Update() {
        Multiplier = Mathf.MoveTowards(Multiplier, targetMultiplier, fadeSpeed * Time.unscaledDeltaTime);

        if (type == SoundType.Sound)
            audioSource.volume = Sound.SoundVolume * Multiplier;
        else
            audioSource.volume = Sound.GetScaledMusicVolume * Multiplier;
    }

}