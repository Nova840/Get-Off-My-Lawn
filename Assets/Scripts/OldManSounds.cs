using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldManSounds : MonoBehaviour {

    [SerializeField]
    private float interval = 10;

    [SerializeField]
    private AudioClip[] clips = null;

    [SerializeField]
    private float timeDelayAfterWaveSound = 3;

    public static float TimeLastSoundPlayed { get; private set; } = Mathf.NegativeInfinity;

    private IEnumerator Start() {
        while (true) {
            yield return new WaitForSeconds(interval);
            float timeSinceLastSoundPlayed = Time.time - TimeLastSoundPlayed;
            while (timeSinceLastSoundPlayed < timeDelayAfterWaveSound) {
                yield return new WaitForSeconds(timeDelayAfterWaveSound - timeSinceLastSoundPlayed);
                timeSinceLastSoundPlayed = Time.time - TimeLastSoundPlayed;
            }
            if (clips.Length > 0)
                Sound.PlaySound(clips[Random.Range(0, clips.Length)]);
        }
    }

    public static void PlaySound(AudioClip clip) {
        TimeLastSoundPlayed = Time.time;
        Sound.PlaySound(clip);
    }

}