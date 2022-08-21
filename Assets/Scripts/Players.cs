using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Players : MonoBehaviour {

    private static List<GameObject> players = new List<GameObject>();

    public static List<GameObject> GetPlayersCopy() {
        return new List<GameObject>(players);
    }

    private void Awake() {
        players.Add(gameObject);
    }

    private void OnDestroy() {
        players.Remove(gameObject);
    }

}