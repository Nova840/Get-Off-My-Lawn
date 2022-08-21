using GameJolt.API;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignInButton : MonoBehaviour {

    [SerializeField]
    private Text text = null;

    [SerializeField]
    private string signedInText = "", signedOutText = "";

    public void OnButtonClick() {
        if (GameJoltAPI.Instance.HasSignedInUser)
            AccessGameJolt.SignOut();
        else
            AccessGameJolt.ShowSignIn();
    }

    private void Update() {
        text.text = GameJoltAPI.Instance.HasSignedInUser ? signedInText : signedOutText;
    }

}