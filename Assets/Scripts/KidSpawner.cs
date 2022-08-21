using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KidSpawner : MonoBehaviour {

    [SerializeField]
    private GameObject kidPrefab = null;

    private BoxCollider2D spawnBox;

    private void Awake() {
        spawnBox = GetComponent<BoxCollider2D>();
    }

    public void SpawnKid(float kidSpeed) {
        if (EndGame.Ended)
            return;

        Transform t = Instantiate(kidPrefab).transform;
        //https://forum.unity.com/threads/randomly-generate-objects-inside-of-a-box.95088/#post-1263920
        Vector2 spawnPosition = new Vector2(
            Random.Range(-spawnBox.size.x, spawnBox.size.x),
            Random.Range(-spawnBox.size.y, spawnBox.size.y)
        );
        spawnPosition = spawnBox.transform.TransformPoint(spawnPosition / 2 + spawnBox.offset);
        t.position = spawnPosition;
        t.GetComponent<MoveKid>().Speed = kidSpeed;
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Kid") && other.GetComponent<MoveKid>().RunningAway)
            Destroy(other.gameObject);
    }

}