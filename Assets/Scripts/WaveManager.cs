using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour {

    [System.Serializable]
    private class WaveData {
        public float timeBetweenWaves = 3;
        [Space]
        public int startingKidsPerWave = 2;
        public float kidsPerWaveStep = 4;
        [Space]
        public float startingKidsInterval = 6;
        public float kidsIntervalStep = 2;
        [Space]
        public float startingKidSpeed = .2f;
        public float kidSpeedStep = .2f;
        [Space]
        public float speedDelta = .4f;
    }

    [SerializeField]
    private WaveData waveData = null;

    [SerializeField]
    private KidSpawner kidSpawner = null;

    public static int WaveNumber { get; private set; } = 0;
    public static int KidsDefeated { get; private set; } = 0;
    public static void AddDefeatedKid() { KidsDefeated++; }

    [SerializeField]
    private Text waveText = null;

    [SerializeField]
    private AudioClip completeWaveSound = null;

    private void Awake() {
        WaveNumber = 0;
        KidsDefeated = 0;
    }

    private void Update() {
        if (!waveInProgress && !EndGame.Ended && MoveKid.AllMoveKidsCopy().All(mk => mk.RunningAway)) {
            WaveNumber++;
            if (WaveNumber > 1)
                OldManSounds.PlaySound(completeWaveSound);
            StartCoroutine(StartWave(WaveNumber));
        }
    }

    private bool waveInProgress = false;
    private IEnumerator StartWave(int wave) {
        waveInProgress = true;

        waveText.gameObject.SetActive(true);
        waveText.text = "Wave " + wave;
        yield return new WaitForSeconds(waveData.timeBetweenWaves);
        waveText.gameObject.SetActive(false);

        yield return StartCoroutine(Wave(
            waveData.startingKidsInterval / (wave == 1 ? 1 : (waveData.kidsIntervalStep * (wave - 1))),
            Mathf.RoundToInt(waveData.kidsPerWaveStep * (wave - 1) + waveData.startingKidsPerWave),
            waveData.kidSpeedStep * (wave - 1) + waveData.startingKidSpeed
        ));

        waveInProgress = false;
    }

    private IEnumerator Wave(float kidsInterval, int kidsInWave, float kidSpeed) {
        float time = 0;
        kidSpawner.SpawnKid(kidSpeed * RandomPercentDelta(waveData.speedDelta));
        int kidsSpawned = 1;
        while (true) {
            yield return new WaitForSeconds(0);
            if (kidsSpawned >= kidsInWave)
                break;

            time += Time.deltaTime;
            int kidsToSpawn = Mathf.FloorToInt(time / kidsInterval);
            time %= kidsInterval;

            for (int i = 0; i < kidsToSpawn; i++) {
                kidSpawner.SpawnKid(kidSpeed * RandomPercentDelta(waveData.speedDelta));
                kidsSpawned++;
            }
        }
    }

    private float RandomPercentDelta(float delta) {
        return Random.Range(1 + delta, 1 - delta);
    }

}