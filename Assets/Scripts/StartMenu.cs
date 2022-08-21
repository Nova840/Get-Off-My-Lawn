using GameJolt.UI;
using GameJolt.UI.Controllers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour {

    [System.Serializable]
    private class Item {
        public StartMenuItem[] items = null;
    }

    [System.Serializable]
    private class Menu {
        public Item[] subMenus = null;
        public int startingSubMenu = 0;
        public int startingSelection = 0;
    }

    [SerializeField]
    private GameObject cursor = null;
    private GameObject holdA;

    [SerializeField]
    private GameObject creditsMenu = null;

    private RectTransform cursorRect;

    [SerializeField]
    private Menu desktopMenus = null, mobileMenus = null;
    private Menu menus;
    private RectTransform[][] itemRects;

    private int currentSubMenu = -1, currentSelection = -1;

    private BinaryInputAxis verticalAxis, horizontalAxis;

    private SignInWindow gameJoltSignInWindow;

    private void InitCursor() {
        if (cursor == null) {
            Debug.LogError("Cursor is null");
            return;
        }
        cursorRect = cursor.GetComponent<RectTransform>();
        if (cursorRect == null) {
            Debug.LogError("Cursor has no RectTransform");
        }
    }

    private void InitItems() {
        menus = Application.isMobilePlatform ? mobileMenus : desktopMenus;
        if (Application.isMobilePlatform) {
            StartMenuItem[] flatMobileMenus = mobileMenus.subMenus.SelectMany(m => m.items).ToArray();
            StartMenuItem[] mobileUnusedItems = desktopMenus.subMenus.SelectMany(m => m.items).Where(i => !flatMobileMenus.Contains(i)).ToArray();
            foreach (StartMenuItem item in mobileUnusedItems)
                Destroy(item.gameObject);
        }
        itemRects = new RectTransform[menus.subMenus.Length][];
        for (int i = 0; i < menus.subMenus.Length; i++) {
            itemRects[i] = new RectTransform[menus.subMenus[i].items.Length];
            for (int j = 0; j < menus.subMenus[i].items.Length; j++) {
                StartMenuItem item = menus.subMenus[i].items[j];
                if (item == null) {
                    Debug.LogErrorFormat("Menu item {0} is null", i);
                    continue;
                }
                RectTransform rect = item.GetComponent<RectTransform>();
                if (rect == null) {
                    Debug.LogErrorFormat("Menu item {0} ({1}) has no RectTransform", i, item.gameObject.name);
                    continue;
                }
                itemRects[i][j] = rect;
            }
        }
    }

    private void Awake() {
        InitCursor();
        InitItems();
        verticalAxis = new BinaryInputAxis("All Joysticks Vertical");
        horizontalAxis = new BinaryInputAxis("All Joysticks Horizontal");
        holdA = cursor.transform.Find("Hold A Background").gameObject;
        gameJoltSignInWindow = GameJoltUI.Instance.transform.Find("SignInPanel").GetComponent<SignInWindow>();
    }

    private void Start() {
        SelectItem(menus.startingSubMenu, menus.startingSelection);
    }

    private void SelectItem(int menuIndex, int selectionIndex) {
        if (currentSubMenu == menuIndex && currentSelection == selectionIndex)
            return;
        menuIndex = Mathf.Clamp(menuIndex, 0, menus.subMenus.Length - 1);
        selectionIndex = Mathf.Clamp(selectionIndex, 0, menus.subMenus[menuIndex].items.Length - 1);
        RectTransform ixform = itemRects[menuIndex][selectionIndex];
        if (ixform != null && cursorRect != null) {
            Rect irect = ixform.rect;
            cursorRect.anchoredPosition = new Vector2(irect.xMin, 0.5f * (irect.yMin + irect.yMax)) + ixform.anchoredPosition;
        }
        currentSubMenu = menuIndex;
        currentSelection = selectionIndex;

        StartMenuItem selected = menus.subMenus[currentSubMenu].items[currentSelection];
        holdA.SetActive(selected.CanAdd() || selected.CanSubtract());
    }

    void Update() {
        verticalAxis.Update();
        horizontalAxis.Update();
        if (Input.GetButtonDown("All Joysticks Button 0"))
            Submit();

        if (CanMoveInMenu()) {
            if (!Input.GetButton("All Joysticks Button 0")) {
                int vertical = verticalAxis.Value;
                if (vertical < 0)
                    SelectItem(currentSubMenu, currentSelection + 1);
                else if (vertical > 0)
                    SelectItem(currentSubMenu, currentSelection - 1);
            }

            int horizontal = horizontalAxis.Value;
            if (horizontal < 0)
                Subtract();
            else if (horizontal > 0)
                Add();
        }

        if (gameJoltSignInWindow.gameObject.activeSelf && Input.GetButtonDown("All Joysticks Button 1"))
            gameJoltSignInWindow.Dismiss(false);

    }

    private bool CanMoveInMenu() {
        return !creditsMenu.activeSelf && !gameJoltSignInWindow.gameObject.activeSelf;
    }

    private void Submit() {
        if (!CanMoveInMenu())
            return;
        StartMenuItem item = menus.subMenus[currentSubMenu].items[currentSelection];
        item.Submit();
    }

    private void Add() {
        if (!CanMoveInMenu())
            return;
        if (!Input.GetButton("All Joysticks Button 0")) {
            MenuUp();
            return;
        }
        StartMenuItem item = menus.subMenus[currentSubMenu].items[currentSelection];
        bool added = item.Add();
        if (!added)
            MenuUp();
    }

    private void Subtract() {
        if (!CanMoveInMenu())
            return;
        if (!Input.GetButton("All Joysticks Button 0")) {
            MenuDown();
            return;
        }
        StartMenuItem item = menus.subMenus[currentSubMenu].items[currentSelection];
        bool subtracted = item.Subtract();
        if (!subtracted)
            MenuDown();
    }

    private void MenuUp() {
        int newMenuIndex = currentSubMenu + 1;
        SelectItem(newMenuIndex, currentSelection);
    }

    private void MenuDown() {
        int newMenuIndex = currentSubMenu - 1;
        SelectItem(newMenuIndex, currentSelection);
    }

}