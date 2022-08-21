using GameJolt.UI;
using GameJolt.UI.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameBackButton : MonoBehaviour {

    private Text text;

    private SignInWindow gameJoltSignInWindow;

    private void Awake() {
        text = GetComponentInChildren<Text>();
        gameJoltSignInWindow = GameJoltUI.Instance.transform.Find("SignInPanel").GetComponent<SignInWindow>();
    }

    private void Update() {
        if (!gameJoltSignInWindow.gameObject.activeSelf)
            if (Input.GetButtonDown("All Joysticks Button 1") || Input.GetButtonDown("K Escape"))
                OnButtonClick();
    }

    public void OnButtonClick() {
        SceneManager.LoadScene("Main Menu");
    }

}