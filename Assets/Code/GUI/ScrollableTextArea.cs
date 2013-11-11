using UnityEngine;
using System.Collections;

/// <summary>
/// Attach this to a button or object to pop up some dialogue that closes on the button press.
/// </summary>
public class ScrollableTextArea : MonoBehaviour {
    /// <summary>
    /// Loads a text file.
    /// </summary>
    public TextAsset file;

    // Font specifics
    public Font font;
    public int fontSize;
    public Color fontColor;
    public Color fontBackground;
    public string closeText;

    // Privates
    private Vector2 scrollPosition;
    private bool drawGUI = false;
    private string text;
    private GUIStyle textStyle;
    private GUIStyle buttonStyle;

	// Use this for initialization
	void Start () {
        text = file.text;
        
        // XXX (kasiu): Checking is not robust
        if (font != null) {
            textStyle = new GUIStyle();
            textStyle.font = font;
            textStyle.fontSize = fontSize;
            textStyle.normal.textColor = fontColor;
            textStyle.wordWrap = true;
            textStyle.border = new RectOffset(20, 20, 20, 20);
            textStyle.normal.background = GUIUtils.MakeBlankTexture((int)(Screen.width / 2.0f), (int)(Screen.height / 2.0f), fontBackground);
        }

        // Defaults
        closeText = (closeText.Length == 0) ? "Close" : closeText;
	}

    void OnGUI() {
        if (drawGUI) {
            // Don't know if this is necessary, but just resets the font in case.
            if (buttonStyle == null) {
                buttonStyle = new GUIStyle(GUI.skin.button);
                buttonStyle.font = font;
            }

            // Sticks this in the center of the screen
            GUILayout.BeginArea(new Rect(Screen.width / 4.0f, Screen.height / 4.0f, Screen.width / 2.0f, Screen.height / 2.0f), GUI.skin.box);

            scrollPosition = GUILayout.BeginScrollView(scrollPosition);
            GUILayout.Label(text, textStyle);
            GUILayout.EndScrollView();
            if(GUILayout.Button(closeText, buttonStyle)) {
                drawGUI = false;
            }
            GUILayout.EndArea();
        }
    }

    void OnMouseDown() {
        drawGUI = true;
    }
}
