using UnityEngine;
using System.Collections;

public class TagLabelGUI : MonoBehaviour {
    /// <summary>
    /// Text size.
    /// </summary>
    public int fontSize = 10;

    /// <summary>
    /// The default font for the label
    /// </summary>
    public Font defaultFont = Resources.Load("gwap_fonts/Nunito-Regular") as Font;
    public Color fontBackground;

    /// <summary>
    /// If the tag name contains spaces, we store the individual words.
    /// </summary>
    public string[] splitName;

    private string newlinedName;

    private GUIStyle textStyle;
    private int maxStringLength;
    private Vector2 textSize;
    private Vector2 padding = new Vector2(10, 10);
    private GUIStyle boxStyle;

	// Use this for initialization
	void Start () {
        // THIS IS A HACK FOR BOTH MODE
        if (this.gameObject.name.Contains("-collab") || this.gameObject.name.Contains("-compete")) {
            int index = this.gameObject.name.IndexOf('-');
            string actualName = this.gameObject.name.Substring(0, index);
            if (this.gameObject.name.Contains("-collab")) {
                actualName += " (HELP)";
            } else {
                actualName += " (COMPETE)";
            }
            splitName = actualName.Split(' ');
        } else {
            splitName = this.gameObject.name.Split(' ');
        }

        newlinedName = "";
        maxStringLength = -1;
        for (int i = 0; i < splitName.Length; i++) {
            newlinedName += splitName[i];
            if (i != splitName.Length - 1) {
                newlinedName += '\n';
            }
            if (splitName[i].Length > maxStringLength) {
                maxStringLength = splitName[i].Length;
            }
        }

        textStyle = null;
	}

    /// <summary>
    /// Draws score on screen
    /// </summary>
    void OnGUI() {
        if (textStyle == null || boxStyle == null) {
            textStyle = new GUIStyle();
            textStyle.font = defaultFont;
            textStyle.fontSize = fontSize;
            textStyle.alignment = TextAnchor.MiddleCenter;
            //textStyle.margin = new RectOffset(20, 20, 20, 20);
//            textStyle.normal.background = GUIUtils.MakeBlankTexture(100, 100, fontBackground);
            textStyle.normal.textColor = Color.black;
            textSize = textStyle.CalcSize(new GUIContent(newlinedName)) + padding;

            boxStyle = new GUIStyle(GUI.skin.box);
            boxStyle.normal.background = GUIUtils.MakeBlankTexture((int)textSize.x * 2, (int)textSize.y * 2, fontBackground);
        }

        //if (GameState.Singleton.TimeUsed <= GameState.Singleton.MaxTime) {
            GUI.depth = (int)GUIDepthLevels.GAME_STATIC;
            Vector3 pixelPosition = Camera.main.WorldToScreenPoint(this.gameObject.transform.position);
            // NOTE (kasiu): Screen.height - pixelPosition.y sets the y position correctly because FOO SCREENSPACE axes reversal :(
            //for (var i = 0; i < splitName.Length; i++) {
            //    Vector2 p = new Vector2(pixelPosition.x, Screen.height + (fontSize * i) - (pixelPosition.y));

            //    // The following centers the font.
            //    // HACK (kasiu): The font "height" is not always correct given the font. Check when using different fonts.
            //    Vector2 tweak = new Vector2(splitName[i].Length * fontSize, fontSize * 2);
            //    tweak /= 2.0f;
            //    p -= tweak;
            //    GUI.Box(new Rect(p.x, p.y, fontSize * splitName[i].Length, fontSize * 2), splitName[i], textStyle); //, style);
            //}
            Vector2 p = new Vector2(pixelPosition.x, Screen.height - pixelPosition.y);            
            GUILayout.BeginArea(new Rect(p.x - (textSize.x / 2), p.y - (textSize.y / 2), textSize.x, textSize.y), boxStyle);
            GUILayout.Label(newlinedName, textStyle);
            GUILayout.EndArea();
            //GUI.Box(new Rect(p.x, p.y, fontSize * maxStringLength, fontSize * 2 * splitName.Length), newlinedName, textStyle); //, style);
        //}
    }
}
