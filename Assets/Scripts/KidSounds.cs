using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidSounds : MonoBehaviour {

    [SerializeField]
    private AudioClip[] hurtSounds = null;

    private static float TimeLastSoundPlayed = Mathf.NegativeInfinity;

    [SerializeField]
    private float timeThreshold = 3;

    public void PlayHurtSound() {
        PlaySound(hurtSounds);
    }

    private void PlaySound(AudioClip[] clips) {
        if (Time.time - TimeLastSoundPlayed > timeThreshold) {
            Sound.PlaySound(clips[Random.Range(0, clips.Length)]);
            TimeLastSoundPlayed = Time.time;
        }
    }

}