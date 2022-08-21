using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ChangeTextForController : MonoBehaviour {

    private Text text;

    [SerializeField]
    private string toDeleteIfNoController = "";

    private void Awake() {
        text = GetComponent<Text>();
    }

    private void Start() {
        SetText();
    }

    private void Update() {
        SetText();
    }

    private void SetText() {
        if (!ShouldDisplayColtrollerText())
            text.text = text.text.Replace(toDeleteIfNoController, "").TrimEnd(' ');
    }


    private static bool ShouldDisplayColtrollerText() {
        return Input.GetJoystickNames().Where(name => name != "").Count() > 0 && !Application.isMobilePlatform;
    }

}