using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZMatchY : MonoBehaviour {

    void Update() {
        Vector3 newPosition = transform.position;
        newPosition.z = newPosition.y;
        transform.position = newPosition;
    }

}