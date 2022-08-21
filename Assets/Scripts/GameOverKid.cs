using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverKid : MonoBehaviour {

    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private float speed = 10;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        transform.position += Vector3.left * speed * Time.deltaTime;
        float xPositionToCamera = MainCamera.Instance.WorldToViewportPoint(transform.position).x;
        if (xPositionToCamera < -.001f) {
            Vector3 newPosition = transform.position;
            newPosition.x = MainCamera.Instance.ViewportToWorldPoint(new Vector3(1 + -xPositionToCamera, 0, 0)).x;//adding the distance off screen prevents kids from forming clear lines at low frame rates
            transform.position = newPosition;
        }
    }

}