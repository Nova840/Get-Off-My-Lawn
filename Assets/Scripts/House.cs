using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class House : MonoBehaviour {

    public static GameObject Reference { get; private set; }

    private int health = 100;

    [SerializeField]
    private Slider healthSlider = null;

    [SerializeField]
    private SpriteRenderer goodHouse = null, mediumHouse = null, badHouse = null;

    [SerializeField]
    private float mediumThreshold = 60, badThreshold = 20;

    [SerializeField]
    private AudioClip houseBreak1 = null, houseBreak2 = null;

    [SerializeField]
    private AudioClip[] kidAttackingNoises = null;

    private static float TimeLastSoundPlayed = Mathf.NegativeInfinity;

    [SerializeField]
    private float kidSoundTimeThreshold = 1;

    private void Awake() {
        Reference = gameObject;
        SetHealthText();
    }

    public void DamageHouse(int damage) {
        health -= damage;
        SetHealthText();
        if (health <= 0 && !EndGame.Ended)
            EndGame.End();
        if (health <= badThreshold) {
            if (badHouse.gameObject.activeSelf == false) {
                ChangeHouses(badHouse);
                Sound.PlaySound(houseBreak2);
            }
        } else if (health <= mediumThreshold) {
            if (mediumHouse.gameObject.activeSelf == false) {
                ChangeHouses(mediumHouse);
                Sound.PlaySound(houseBreak1);
            }
        }
        PlaySound(kidAttackingNoises);
    }

    private void PlaySound(AudioClip[] clips) {
        if (Time.time - TimeLastSoundPlayed > kidSoundTimeThreshold) {
            Sound.PlaySound(clips[Random.Range(0, clips.Length)]);
            TimeLastSoundPlayed = Time.time;
        }
    }

    private void ChangeHouses(SpriteRenderer sr) {
        if (badHouse.gameObject.activeSelf != (sr == badHouse))
            badHouse.gameObject.SetActive(sr == badHouse);
        if (mediumHouse.gameObject.activeSelf != (sr == mediumHouse))
            mediumHouse.gameObject.SetActive(sr == mediumHouse);
        if (goodHouse.gameObject.activeSelf != (sr == goodHouse))
            goodHouse.gameObject.SetActive(sr == goodHouse);
    }

    private void SetHealthText() {
        healthSlider.value = Mathf.Clamp(health, 0, 100);
        if (healthSlider.value == 0)
            healthSlider.fillRect.gameObject.SetActive(false);
    }

}