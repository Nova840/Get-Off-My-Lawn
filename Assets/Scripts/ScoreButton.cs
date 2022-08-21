using GameJolt.API;
using GameJolt.UI;
using GameJolt.UI.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ScoreButton : MonoBehaviour {

    private Button button;
    private Text text;

    [SerializeField, TextArea(3, 5)]
    private string signedInString = "", notSignedInString = "", scoreSubmittedString = "", score0String = "", moreThan1PlayerString = "";

    private SignInWindow gameJoltSignInWindow;

    public static bool ScoreSubmitted { get; private set; } = false;

    private void Awake() {
        button = GetComponent<Button>();
        text = GetComponentInChildren<Text>();
        ScoreSubmitted = false;
        gameJoltSignInWindow = GameJoltUI.Instance.transform.Find("SignInPanel").GetComponent<SignInWindow>();
    }

    private void Update() {
        string newText = "";
        if (PlayerCount.NumPlayers > 1) {
            newText = moreThan1PlayerString;
        } else {
            if (!ScoreSubmitted) {
                newText = GameJoltAPI.Instance.HasSignedInUser ? signedInString : notSignedInString;
                string userName = GameJoltAPI.Instance.HasSignedInUser ? GameJoltAPI.Instance.CurrentUser.Name : "No User";
                newText = newText.Replace("<user>", userName);
            } else {
                button.interactable = false;
                newText = WaveManager.KidsDefeated > 0 ? scoreSubmittedString : score0String;
            }
        }

        text.text = newText;

        if (EndGame.Ended)
            CheckSubmit(true);
    }

    public void OnButtonClick() {
        CheckSubmit(false);
    }

    private void CheckSubmit(bool checkInput) {
        if (!gameJoltSignInWindow.gameObject.activeSelf) {
            if (PlayerCount.NumPlayers == 1) {
                if (!checkInput || Input.GetButtonDown("All Joysticks Button 2")) {
                    if (GameJoltAPI.Instance.HasSignedInUser) {
                        if (!ScoreSubmitted) {
                            if (WaveManager.KidsDefeated > 0) {
                                AccessGameJolt.SubmitScore(WaveManager.KidsDefeated);
                            }
                            ScoreSubmitted = true;
                        }
                    } else {
                        AccessGameJolt.ShowSignIn();
                    }
                }
            }
        } else {
            if (!checkInput || Input.GetButtonDown("All Joysticks Button 1")) {
                gameJoltSignInWindow.Dismiss(false);
            }
        }
    }

}