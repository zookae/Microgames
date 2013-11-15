using UnityEngine;
using System.Collections;

public class NextSceneBox : MonoBehaviour {
    public TextAsset file;
    public string leftButtonText;
    public string rightButtonText;
    public string sceneName;

    public int width;
    public int height;

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
    private GUIStyle boxStyle;
    private GUIStyle buttonStyle;

	// Use this for initialization
	void Start () {
        text = file.text;

        // XXX (kasiu): CHECKING IS STILL NOT ROBUST :P
        if (font != null) {
            textStyle = new GUIStyle();
            textStyle.font = font;
            textStyle.fontSize = fontSize;
            textStyle.normal.textColor = fontColor;
            textStyle.alignment = TextAnchor.MiddleCenter;
            textStyle.wordWrap = true;
            textStyle.border = new RectOffset(20, 20, 20, 20);
        }

        // Defaults
        width = (width <= 0 || width > Screen.width) ? 400 : width;
        height = (height <= 0 || height > Screen.height) ? 200 : height;
        leftButtonText = (leftButtonText.Length == 0) ? "Continue" : leftButtonText;
        rightButtonText = (rightButtonText.Length == 0) ? "Back" : rightButtonText;	
	}
	
	// Update is called once per frame
	void OnGUI () {
        if (drawGUI) {
            // Don't know if this is necessary, but just resets the font in case.
            if (boxStyle == null && buttonStyle == null) {
                buttonStyle = new GUIStyle(GUI.skin.button);
                buttonStyle.font = font;
                boxStyle = new GUIStyle(GUI.skin.box);               
                boxStyle.normal.background = GUIUtils.MakeBlankTexture(width, height, fontBackground);
            }

            GUI.depth = (int)GUIDepthLevels.INFO_WINDOW;
            Vector2 position = GUIUtils.ComputeCenteredPosition(width, height);
            GUILayout.BeginArea(new Rect(position.x, position.y, width, height), GUI.skin.box);

            scrollPosition = GUILayout.BeginScrollView(scrollPosition, boxStyle);
            GUILayout.Label(text, textStyle);
            GUILayout.EndScrollView();

            // BUTTONS
            GUILayout.BeginHorizontal();
            if (GUILayout.Button(leftButtonText, buttonStyle)) {
                drawGUI = false;
                Application.LoadLevel(sceneName);
            }
            if (GUILayout.Button(rightButtonText, buttonStyle)) {
                drawGUI = false;
            }
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }
	}

    void OnMouseDown() {
        drawGUI = true;
    }
}
