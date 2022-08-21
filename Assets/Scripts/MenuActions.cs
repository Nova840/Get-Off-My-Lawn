using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuActions : MonoBehaviour {

    [SerializeField]
    private GameObject credits = null;

    [SerializeField]
    private PlayerCount playerCount = null;

    public void StartGame() {
        if (!playerCount.EnoughPlayers())
            return;
        SceneManager.LoadScene("Game");
    }

    public void ActivateCreditsMenu() {
        credits.SetActive(true);
    }

}
