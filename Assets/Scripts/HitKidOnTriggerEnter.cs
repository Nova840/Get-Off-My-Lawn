using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitKidOnTriggerEnter : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Kid")) {
            other.GetComponent<MoveKid>().GetHit();
        }
    }

}