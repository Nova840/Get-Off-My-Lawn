using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGameOverKids : MonoBehaviour {

    [SerializeField]
    private GameObject gameOverKidPrefab = null;

    [SerializeField]
    private BoxCollider2D spawnBox = null;

    [SerializeField]
    private int numKidsToSpawnDesktop = 200, numKidsToSpawnMobile = 50;

    private List<GameObject> allGameOverKids = new List<GameObject>();

    private void Start() {
        for (int i = 0; i < (Application.isMobilePlatform ? numKidsToSpawnMobile : numKidsToSpawnDesktop); i++) {
            GameObject g = Instantiate(gameOverKidPrefab);
            allGameOverKids.Add(g);
            g.SetActive(false);
            Transform t = g.transform;

            //https://forum.unity.com/threads/randomly-generate-objects-inside-of-a-box.95088/#post-1263920
            Vector2 spawnPosition = new Vector2(
                Random.Range(-spawnBox.size.x, spawnBox.size.x),
                Random.Range(-spawnBox.size.y, spawnBox.size.y)
            );
            spawnPosition = spawnBox.transform.TransformPoint(spawnPosition / 2 + spawnBox.offset);
            t.position = spawnPosition;

            t.parent = transform;
        }
    }

    public void EnableAll() {
        foreach (GameObject g in allGameOverKids)
            g.SetActive(true);
    }

}