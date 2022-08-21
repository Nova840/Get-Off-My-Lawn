using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Sound {

    public static float SoundVolume { get; set; } = 1;
    public static float MusicVolume { get; set; } = 1;
    private const float MusicVolumeMultiplier = .4f;
    public static float GetScaledMusicVolume {
        get {
            return MusicVolume * MusicVolumeMultiplier;
        }
    }

    private static GameObject soundPrefab;
    private static GameObject SoundPrefab {
        get {
            if (!soundPrefab)
                soundPrefab = Resources.Load("Sound") as GameObject;
            return soundPrefab;
        }
    }

    public static void PlaySound(AudioClip clip, float volume = 1) {
        GameObject g = Object.Instantiate(SoundPrefab);
        AudioSource source = g.GetComponent<AudioSource>();
        source.clip = clip;
        source.volume = volume;
        source.Play();
    }

}