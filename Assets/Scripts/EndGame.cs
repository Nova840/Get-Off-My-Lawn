using GameJolt.UI;
using GameJolt.UI.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour {

    private static EndGame Instance;

    private static float startTime;

    public static bool Ended { get; private set; } = false;

    [SerializeField]
    private GameObject gameOverScreen = null;

    [SerializeField]
    private SpawnGameOverKids spawnGameOverKids = null;

    [SerializeField]
    private AudioClip[] loseSounds = null;

    private void Awake() {
        startTime = Time.time;
        Ended = false;
        Instance = this;
    }

    public static float GetTime() {
        return Time.time - startTime;
    }

    public static void End() {
        Ended = true;
        Instance.gameOverScreen.SetActive(true);
        Instance.spawnGameOverKids.EnableAll();
        OldManSounds.PlaySound(Instance.loseSounds[Random.Range(0, Instance.loseSounds.Length)]);
    }

}