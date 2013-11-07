using UnityEngine;
using System.Collections;

public class MouseHoverTooltip : MonoBehaviour {
    public int fontSize;
    public Color fontColor;
    public string text;

    private bool active = false;

    void OnMouseOver() {
        active = true;
    }

    void OnMouseExit() {
        active = false;
    }

    void OnGUI() {
        if (active) {
            GUIStyle style = new GUIStyle();
            style.fontSize = fontSize;
            style.normal.textColor = fontColor;

            GUI.Box(new Rect(Input.mousePosition.x + fontSize, ((Screen.height - Input.mousePosition.y)), fontSize * text.Length, fontSize * 2), text, style);
        }
    }
}
