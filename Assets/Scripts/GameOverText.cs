using GameJolt.API;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class GameOverText : MonoBehaviour {

    private Text text;

    private void Awake() {
        text = GetComponent<Text>();
    }

    private void Update() {
        string newText = text.text;
        newText = newText.Replace("<waves>", (WaveManager.WaveNumber - 1).ToString());
        newText = newText.Replace("<kids>", WaveManager.KidsDefeated.ToString());
        text.text = newText;
    }

}