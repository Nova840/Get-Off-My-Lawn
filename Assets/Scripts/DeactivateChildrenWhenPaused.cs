using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeactivateChildrenWhenPaused : MonoBehaviour {

    private GameObject[] children;

    private void Awake() {
        children = GetComponentsInChildren<Transform>().Where(t => t != transform).Select(t => t.gameObject).ToArray();
    }

    private void Update() {
        foreach (GameObject child in children)
            if (PauseGame.Paused == child.activeSelf)
                child.SetActive(!PauseGame.Paused);
    }

}