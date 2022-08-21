using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnPlayers : MonoBehaviour {

    [SerializeField]
    private GameObject playerPrefab = null;

    [SerializeField]
    private Transform spawnpointsContainer = null;
    private Transform[] spawnpoints = null;

    private void Awake() {
        spawnpoints = spawnpointsContainer.GetComponentsInChildren<Transform>().Where(t => t.parent == spawnpointsContainer).ToArray();
    }

    private void Start() {
        int playerCount = Mathf.Max(PlayerCount.NumPlayers, 1);
        List<Transform> spawnpointsCopy = new List<Transform>(spawnpoints);
        for (int i = 1; i <= playerCount; i++) {

            int randomIndex = Random.Range(0, spawnpointsCopy.Count);
            GameObject player = Instantiate(playerPrefab, spawnpointsCopy[randomIndex].position, Quaternion.identity);
            spawnpointsCopy.RemoveAt(randomIndex);

            if (Player1UsesKeyboard.On) {
                if (i == 1) {
                    player.GetComponent<PlayerMove>().Player = "K";
                    player.GetComponent<PlayerMelee>().Player = "K";

                } else {
                    player.GetComponent<PlayerMove>().Player = "P" + (i - 1);
                    player.GetComponent<PlayerMelee>().Player = "P" + (i - 1);

                }
            } else {
                player.GetComponent<PlayerMove>().Player = "P" + i;
                player.GetComponent<PlayerMelee>().Player = "P" + i;

            }

        }
    }

}