using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCount : MonoBehaviour {

    public static int NumPlayers { get; private set; } = 1;

    [SerializeField]
    private InputField displayInputField = null;

    [SerializeField]
    private Text displayInputFieldText = null;

    [SerializeField]
    private Toggle keyboardToggle = null;

    [SerializeField]
    private Color enoughPlayersColor = Color.black, notEnoughPlayersColor = Color.red;

    private void Start() {
        SetInputField();
    }

    public void AddPlayer() {
        NumPlayers++;
        NumPlayers = Mathf.Clamp(NumPlayers, 1, 16);
        SetInputField();
    }

    public void SubtractPlayer() {
        NumPlayers--;
        NumPlayers = Mathf.Clamp(NumPlayers, 1, 16);
        SetInputField();
    }

    public bool EnoughPlayers() {
        int availablePlayers = Input.GetJoystickNames().Count(c => c != "");
        if (keyboardToggle.isOn)
            availablePlayers++;
        return NumPlayers <= availablePlayers;
    }

    private void SetInputField() {
        displayInputField.text = NumPlayers.ToString();
    }

    private void Update() {
        if (!displayInputFieldText)
            return;
        if (EnoughPlayers())
            displayInputFieldText.color = enoughPlayersColor;
        else
            displayInputFieldText.color = notEnoughPlayersColor;
    }

}