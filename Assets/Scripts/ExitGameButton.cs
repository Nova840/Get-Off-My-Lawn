using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Selectable))]
public class ExitGameButton : MonoBehaviour {

    [SerializeField]
    private Color webglColor = new Color(.8f, .8f, .8f);

    private void Awake() {
        if (Application.platform == RuntimePlatform.WebGLPlayer) {
            Selectable selectable = GetComponent<Selectable>();
            selectable.interactable = false;
            ColorBlock colors = selectable.colors;
            colors.disabledColor = webglColor;
            selectable.colors = colors;
            GetComponent<UninteractableWhenCreditsMenuActive>().enabled = false;
        }
    }

    public void ExitGame() {
        Application.Quit();
    }

}