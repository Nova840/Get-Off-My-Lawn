using GameJolt.API;
using GameJolt.API.Objects;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreTable : MonoBehaviour {

    [SerializeField]
    private Text[] rankTexts = null, nameTexts = null, scoreTexts = null;

    private const int NumScoresToGet = 11;

    [SerializeField]
    private Color normalColor = Color.black, currentPlayerColor = Color.black;

    private static Score[] lastReceivedScores;
    private static Score[] lastRecievedCurrentPlayerScores;//should only have a length of 1
    private static int lastRecievedPlayerRank = 0;

    public static HighScoreTable Instance { get; private set; }

    private enum TextType {
        Ranks, Names, Scores
    }

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        SetLastReceivedScores();
    }

    public static void SetLastReceivedScores() {
        Scores.Get(scores => { lastReceivedScores = scores; }, 0, NumScoresToGet);
    }

    public static void SetCurrentPlayerScore() {
        if (!GameJoltAPI.Instance.HasSignedInUser) {
            lastRecievedCurrentPlayerScores = null;
            lastRecievedPlayerRank = 0;
            return;
        }
        Scores.Get(scores => {
            if (scores != null && scores.Length > 0) {
                Scores.GetRank(scores[0].Value, 0, rank => {
                    lastRecievedCurrentPlayerScores = scores;
                    lastRecievedPlayerRank = rank;
                });
            } else {
                lastRecievedCurrentPlayerScores = null;
                lastRecievedPlayerRank = 0;
            }
        }, 0, 1, true);
    }

    private void SetText(TextType type) {
        if (lastReceivedScores == null)
            return;
        for (int i = 0; i < rankTexts.Length; i++) {
            if (i >= lastReceivedScores.Length)
                break;
            if (type == TextType.Names)
                nameTexts[i].text = lastReceivedScores[i].PlayerName;
            else if (type == TextType.Scores)
                scoreTexts[i].text = lastReceivedScores[i].Value.ToString();
            else if (type == TextType.Ranks)
                rankTexts[i].text = (i + 1) + ":";
        }
    }

    private void Update() {
        SetText(TextType.Names);
        SetText(TextType.Scores);
        SetText(TextType.Ranks);

        if (lastRecievedCurrentPlayerScores != null && !AnyNameTextMatchesUser()) {
            nameTexts[nameTexts.Length - 1].text = lastRecievedCurrentPlayerScores[0].PlayerName;
            scoreTexts[scoreTexts.Length - 1].text = lastRecievedCurrentPlayerScores[0].Value.ToString();
            rankTexts[rankTexts.Length - 1].text = lastRecievedPlayerRank + ":";
        }

        for (int i = 0; i < rankTexts.Length; i++) {
            if (NameTextMatchesUser(i)) {
                nameTexts[i].color = currentPlayerColor;
                scoreTexts[i].color = currentPlayerColor;
                rankTexts[i].color = currentPlayerColor;
            } else {
                nameTexts[i].color = normalColor;
                scoreTexts[i].color = normalColor;
                rankTexts[i].color = normalColor;
            }
        }

    }

    private bool NameTextMatchesUser(int index) {
        return GameJoltAPI.Instance.HasSignedInUser && nameTexts[index].text == GameJoltAPI.Instance.CurrentUser.Name;
    }

    private bool AnyNameTextMatchesUser() {
        return GameJoltAPI.Instance.HasSignedInUser && nameTexts.Any(t => t.text == GameJoltAPI.Instance.CurrentUser.Name);
    }

}