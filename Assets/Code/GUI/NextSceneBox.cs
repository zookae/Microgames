using UnityEngine;
using System.Collections;

public class NextSceneBox : MonoBehaviour {
    public TextAsset file;
    public string leftButtonText;
    public string rightButtonText;
    public string sceneName;

    // HACK (kasiu): Doesn't currently check dimensions.
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
    private GUIStyle textStyle;
    private string text;
    private Font oldButtonFont;

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
            textStyle.normal.background = GUIUtils.MakeBlankTexture((int)(Screen.width / 2.0f), (int)(Screen.height / 2.0f), fontBackground);
        }

        // Defaults
        width = (width < 0 || width > Screen.width) ? 400 : width;
        height = (height < 0 || height > Screen.height) ? 200 : height;
        leftButtonText = (leftButtonText.Length == 0) ? "Continue" : leftButtonText;
        rightButtonText = (rightButtonText.Length == 0) ? "Back" : rightButtonText;	
	}
	
	// Update is called once per frame
	void OnGUI () {
        if (drawGUI) {
            // Don't know if this is necessary, but just resets the font in case.
            if (oldButtonFont == null) {
                oldButtonFont = GUI.skin.button.font;
                GUI.skin.button.font = font;
            }

            GUILayout.BeginArea(new Rect((Screen.width - width) / 2.0f, (Screen.height - height) / 2.0f, width, height), GUI.skin.box);

            scrollPosition = GUILayout.BeginScrollView(scrollPosition);
            GUILayout.Label(text, textStyle);
            GUILayout.EndScrollView();

            // BUTTONS
            GUILayout.BeginHorizontal();
            if (GUILayout.Button(leftButtonText)) {
                GUI.skin.button.font = oldButtonFont;
                drawGUI = false;
                Application.LoadLevel(sceneName);
            }
            if (GUILayout.Button(rightButtonText)) {
                GUI.skin.button.font = oldButtonFont;
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
