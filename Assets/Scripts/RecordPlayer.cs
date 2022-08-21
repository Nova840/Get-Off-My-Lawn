using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RecordPlayer : MonoBehaviour {

    private bool musicPlaying = false;

    [SerializeField]
    private float duration = 10;
    private float timePlayed = 0;

    [SerializeField]
    private ParticleSpawner particleSpawner = null;

    [SerializeField]
    private AudioSource music = null;

    private static List<RecordPlayer> allRecordPlayers = new List<RecordPlayer>();

    public ParticleSpawner GetParticleSpawner() {
        return particleSpawner;
    }

    public void PlayMusic() {
        if (musicPlaying == true)
            return;
        musicPlaying = true;
        BackgroundMusicVolume.Instance.FadeToVolume(0);
        music.Play();
    }

    private void Update() {
        if (musicPlaying)
            timePlayed += Time.deltaTime;
        if (timePlayed >= duration) {
            Destroy(gameObject);
            if (allRecordPlayers.All(r => !r.musicPlaying || r == this))
                BackgroundMusicVolume.Instance.FadeToVolume(1);
        }
    }

    private void Awake() {
        allRecordPlayers.Add(this);
    }

    private void OnDestroy() {
        allRecordPlayers.Remove(this);
    }

}