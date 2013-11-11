using UnityEngine;
using System.Collections;

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

    private Vector2 scrollPosition;
    private bool drawGUI = false;
    private string text;
    private GUIStyle textStyle;

    private Font oldButtonFont;

	// Use this for initialization
	void Start () {
        text = file.text;
        if (font != null) {
            textStyle = new GUIStyle();
            textStyle.font = font;
            textStyle.fontSize = fontSize;
            textStyle.normal.textColor = fontColor;
            textStyle.wordWrap = true;
            textStyle.border = new RectOffset(20, 20, 20, 20);
            textStyle.normal.background = MakeDummyBlankTexture((int)(Screen.width / 2.0f), (int)(Screen.height / 2.0f), fontBackground);
        }



        // Defaults
        closeText = (closeText.Length == 0) ? "Close" : closeText;
	}

    void OnGUI() {
        if (drawGUI) {
            // Don't know if this is necessary, but just resets the font in case.
            if (oldButtonFont == null) {
                oldButtonFont = GUI.skin.button.font;
            }

            // Sticks this in the center of the screen
            GUILayout.BeginArea(new Rect(Screen.width / 4.0f, Screen.height / 4.0f, Screen.width / 2.0f, Screen.height / 2.0f), GUI.skin.box);
            GUI.skin.button.font = font;

            scrollPosition = GUILayout.BeginScrollView(scrollPosition);

            GUILayout.Label(text, textStyle);
            GUILayout.EndScrollView();
            if(GUILayout.Button(closeText)) {
                drawGUI = false;
                GUI.skin.button.font = oldButtonFont;
            }
            GUILayout.EndArea();
        }
    }

    void OnMouseDown() {
        drawGUI = true;
    }
	
	// Update is called once per frame
	void Update () {
	
	}


    // A STUPID HACK
    // http://forum.unity3d.com/threads/66015-Changing-the-Background-Color-for-BeginHorizontal
    private Texture2D MakeDummyBlankTexture(int width, int height, Color col) {
        Color[] pixels = new Color[width * height];

        for (int i = 0; i < pixels.Length; i++) {
            pixels[i] = col;
        }
        Texture2D tex = new Texture2D(width, height);
        tex.SetPixels(pixels);
        tex.Apply();

        return tex;
    }
}
