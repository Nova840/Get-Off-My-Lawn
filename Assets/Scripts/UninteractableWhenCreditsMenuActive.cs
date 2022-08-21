using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Selectable))]
public class UninteractableWhenCreditsMenuActive : MonoBehaviour {

    [SerializeField]
    private GameObject creditsMenu = null;

    private Selectable selectable;

    private void Awake() {
        selectable = GetComponent<Selectable>();
    }

    private void Update() {
        selectable.interactable = !creditsMenu.activeSelf;
    }

}