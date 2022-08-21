using GameJolt.API;
using GameJolt.UI;
using System.Collections;
using UnityEngine;

public static class AccessGameJolt {

    public static void ShowSignIn() {
        if (GameJoltAPI.Instance.HasSignedInUser)
            return;
        GameJoltUI.Instance.ShowSignIn(success => {
            if (success) {
                GameJoltAPI.Instance.StartCoroutine(GetAvatar());
                HighScoreTable.SetCurrentPlayerScore();
            }
        });
    }

    public static void SignOut() {
        if (!GameJoltAPI.Instance.HasSignedInUser)
            return;
        string lastUserName = GameJoltAPI.Instance.CurrentUser.Name;
        Sprite lastAvatar = GameJoltAPI.Instance.CurrentUser.Avatar;
        GameJoltAPI.Instance.CurrentUser.SignOut();
        GameJoltUI.Instance.QueueNotification("Signed Out " + lastUserName, lastAvatar);
        HighScoreTable.SetCurrentPlayerScore();
    }

    private static IEnumerator GetAvatar() {
        while (GameJoltAPI.Instance.CurrentUser.AvatarURL == null)
            yield return new WaitForSeconds(0);
        GameJoltAPI.Instance.CurrentUser.DownloadAvatar(NotifySignIn);
    }

    private static void NotifySignIn(bool success) {
        if (GameJoltAPI.Instance.HasSignedInUser)//in case they sign out very quickly
            GameJoltUI.Instance.QueueNotification("Welcome " + GameJoltAPI.Instance.CurrentUser.Name + "!", success ? GameJoltAPI.Instance.CurrentUser.Avatar : GameJoltAPI.Instance.DefaultNotificationIcon);
    }

    public static void SubmitScore(int score) {
        if (!GameJoltAPI.Instance.HasSignedInUser)
            return;
        Scores.Add(score, score.ToString(), 0, "", UpdateScoreTable);
    }

    private static void UpdateScoreTable(bool success) {
        if (success && HighScoreTable.Instance) {
            HighScoreTable.SetLastReceivedScores();
            HighScoreTable.SetCurrentPlayerScore();
        }
    }

}