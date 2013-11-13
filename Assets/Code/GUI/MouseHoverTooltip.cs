using UnityEngine;
using System.Collections;

public class MouseHoverTooltip : MonoBehaviour {
    public int fontSize;
    public Color fontColor;
    public Color fontBackground;
    public string text;

    private GUIStyle style;
    private bool active = false;

    void Start() {
        style = null;
    }

    void OnMouseOver() {
        active = true;
    }

    void OnMouseExit() {
        active = false;
    }

    void OnGUI() {
        if (style == null) {
            style = new GUIStyle(GUI.skin.label);
            style.fontSize = fontSize;
            style.normal.textColor = fontColor;
            style.alignment = TextAnchor.MiddleCenter;
            style.normal.background = GUIUtils.MakeBlankTexture(100, 100, fontBackground);
        }

        if (active) {
            //GUI.Box(new Rect(Input.mousePosition.x + fontSize, ((Screen.height - Input.mousePosition.y)), fontSize * text.Length, fontSize * 2), text, style);
            GUILayout.BeginArea(new Rect(Input.mousePosition.x + fontSize, ((Screen.height - Input.mousePosition.y)), fontSize * text.Length, fontSize * 2));
            GUILayout.Label(text, style);
            GUILayout.EndArea();
        }
    }
}
