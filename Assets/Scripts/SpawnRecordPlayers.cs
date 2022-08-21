using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRecordPlayers : MonoBehaviour {

    [SerializeField]
    private GameObject recordPlayerPrefab = null;

    [SerializeField]
    private float spawnInterval = 10;

    private BoxCollider2D spawnBox = null;

    private void Awake() {
        spawnBox = GetComponent<BoxCollider2D>();
    }

    private IEnumerator Start() {
        float time = 0;
        while (true) {
            yield return new WaitForSeconds(0);
            if (EndGame.Ended)
                break;
            time += Time.deltaTime;
            int numSpawns = Mathf.FloorToInt(time / spawnInterval);
            time %= spawnInterval;

            for (int i = 0; i < numSpawns; i++)
                Spawn();
        }
    }

    private void Spawn() {
        //https://forum.unity.com/threads/randomly-generate-objects-inside-of-a-box.95088/#post-1263920
        Vector2 spawnPosition = new Vector2(
            Random.Range(-spawnBox.size.x, spawnBox.size.x),
            Random.Range(-spawnBox.size.y, spawnBox.size.y)
        );
        spawnPosition = spawnBox.transform.TransformPoint(spawnPosition / 2 + spawnBox.offset);
        Instantiate(recordPlayerPrefab, spawnPosition, Quaternion.identity);
    }

}