using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class KidsDefeatedText : MonoBehaviour {

    private Text text;

    [SerializeField, TextArea()]
    private string kidsDefeatedString = "";

    private void Awake() {
        text = GetComponent<Text>();
    }

    private void Update() {
        text.text = Regex.Replace(kidsDefeatedString, "<kids>", WaveManager.KidsDefeated.ToString());
    }

}