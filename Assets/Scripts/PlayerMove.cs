using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    [SerializeField]
    private float speed = 10, mobileSensitivity = .5f;

    private Rigidbody2D rb;

    public string Player { get; set; }

    [SerializeField]
    private GameObject runningLegs = null, standingLegs = null;

    private Renderer[] runningRenderers, standingRenderers;

    private float originalLocalScaleX;

    private Vector2 stickDimensions, stickBackgroundDimensoins;

    private int fingerId = -1;
    private Vector2 touchStartPosition = Vector2.zero;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        originalLocalScaleX = transform.localScale.x;
        runningRenderers = runningLegs.GetComponentsInChildren<Renderer>();
        standingRenderers = standingLegs.GetComponentsInChildren<Renderer>();
        stickDimensions = GetDimensions(InputCanvas.Stick);
        stickBackgroundDimensoins = GetDimensions(InputCanvas.StickBackground);
        touchStartPosition = stickBackgroundDimensoins / 2;
    }

    private void Update() {
        if (PauseGame.Paused)
            return;
        Vector2 move;
        if (!EndGame.Ended)
            move = Application.isMobilePlatform ? GetMobileMoveVector() : GetDesktopMoveVector();
        else
            move = Vector2.zero;
        rb.velocity = move;

        foreach (Renderer r in runningRenderers)
            if (r.enabled != (move != Vector2.zero))
                r.enabled = move != Vector2.zero;
        foreach (Renderer r in standingRenderers)
            if (r.enabled != (move == Vector2.zero))
                r.enabled = move == Vector2.zero;

        if (move.x < 0)
            transform.localScale = new Vector3(-originalLocalScaleX, transform.localScale.y, transform.localScale.z);
        else if (move.x > 0)
            transform.localScale = new Vector3(originalLocalScaleX, transform.localScale.y, transform.localScale.z);
    }

    private Vector2 GetDesktopMoveVector() {
        Vector2 direction;
        direction.x = Input.GetAxisRaw(Player + " Horizontal");
        direction.y = Input.GetAxisRaw(Player + " Vertical");
        return Vector2.ClampMagnitude(direction, 1) * speed;
    }

    private Vector2 GetMobileMoveVector() {
        Vector2 direction = Vector2.zero;
        if (GetCurrentTouch(out Vector2 currentPosition, out Vector2 startPosition))
            direction = MainCamera.Instance.ScreenToWorldPoint(currentPosition) - MainCamera.Instance.ScreenToWorldPoint(startPosition);
        else
            direction = Vector2.zero;
        Vector2 vector = Vector2.ClampMagnitude(direction, 1 / mobileSensitivity) * mobileSensitivity * speed;

        InputCanvas.StickBackground.position = startPosition;
        InputCanvas.Stick.position = Vector2.ClampMagnitude(currentPosition - startPosition, stickBackgroundDimensoins.x / 2 - stickDimensions.x / 2) + startPosition;

        return vector;
    }

    private bool GetCurrentTouch(out Vector2 currentPosition, out Vector2 startPosition) {
        Touch[] allTouches = Input.touches;
        Touch[] leftSideTouches = allTouches.Where(t => MainCamera.Instance.ScreenToViewportPoint(t.position).x < .5f).ToArray();

        if (fingerId != -1) {//was a touch last frame
            Touch[] currentMatchArray = allTouches.Where(t => t.fingerId == fingerId).ToArray();//should be either 0 or 1 in length
            if (currentMatchArray.Length > 0) {//is matching touch this frame
                SetExistingTouch(currentMatchArray, out currentPosition, out startPosition);
                return true;
            } else {//is no matching touch this frame
                return SetNewTouch(leftSideTouches, out currentPosition, out startPosition);
            }
        } else {//was no touch last frame
            return SetNewTouch(leftSideTouches, out currentPosition, out startPosition);
        }

    }

    private bool SetNewTouch(Touch[] leftSideTouches, out Vector2 currentPosition, out Vector2 startPosition) {
        if (leftSideTouches.Length > 0) {//is a touch this frame
            Touch currentTouch = leftSideTouches[0];
            currentPosition = currentTouch.position;
            touchStartPosition = startPosition = currentTouch.position;
            fingerId = currentTouch.fingerId;
            return true;
        } else {//is no touch this frame
            currentPosition = touchStartPosition;
            startPosition = touchStartPosition;
            fingerId = -1;
            return false;
        }
    }

    private void SetExistingTouch(Touch[] currentMatchArray, out Vector2 currentPosition, out Vector2 startPosition) {
        Touch currentTouch = currentMatchArray[0];
        currentPosition = currentTouch.position;
        startPosition = touchStartPosition;
        fingerId = currentTouch.fingerId;
    }

    private Vector2 GetDimensions(RectTransform rt) {
        Vector3[] corners = new Vector3[4];
        rt.GetWorldCorners(corners);
        float width = Mathf.Abs(corners[0].x - corners[3].x);
        float height = Mathf.Abs(corners[0].y - corners[1].y);
        return new Vector2(width, height);
    }

}