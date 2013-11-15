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

    /// <summary>
    /// Width and height of the text area.
    /// </summary>
    public int width = -1;
    public int height = -1;

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
    private GUIStyle boxStyle;

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
        }

        width = (width <= 0) ? Screen.width / 2 : width;
        height = (height <= 0) ? Screen.height / 2 : height;

        // Defaults
        closeText = (closeText.Length == 0) ? "Close" : closeText;
	}

    void OnGUI() {
        if (drawGUI) {
            // Don't know if this is necessary, but just resets the font in case.
            if (buttonStyle == null) {
                buttonStyle = new GUIStyle(GUI.skin.button);
                buttonStyle.font = font;
                boxStyle = new GUIStyle(GUI.skin.box);
                boxStyle.normal.background = GUIUtils.MakeBlankTexture(width, height, fontBackground);
            }

            // Sticks this in the center of the screen
            GUI.depth = (int)GUIDepthLevels.INFO_WINDOW;
            Vector2 position = GUIUtils.ComputeCenteredPosition(width, height);
            GUILayout.BeginArea(new Rect(position.x, position.y, width, height), GUI.skin.box);

            scrollPosition = GUILayout.BeginScrollView(scrollPosition, boxStyle);
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
