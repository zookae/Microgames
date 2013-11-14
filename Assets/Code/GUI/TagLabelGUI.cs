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
    public Font defaultFont = Resources.Load("../Fonts/Nunito-Regular") as Font;

    /// <summary>
    /// If the tag name contains spaces, we store the individual words.
    /// </summary>
    public string[] splitName;

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
	}

    /// <summary>
    /// Draws score on screen
    /// </summary>
    void OnGUI() {
        // Sets up style
        GUIStyle style = new GUIStyle();
        style.fontSize = fontSize;
        style.alignment = TextAnchor.MiddleCenter;
        if (defaultFont != null) {
            style.font = defaultFont;
        }

        Vector3 pixelPosition = Camera.main.WorldToScreenPoint(this.gameObject.transform.position);
        // NOTE (kasiu): Screen.height - pixelPosition.y sets the y position correctly because FOO SCREENSPACE axes reversal :(
        for (var i = 0; i < splitName.Length; i++) {
            Vector2 p = new Vector2(pixelPosition.x, Screen.height + (fontSize * i) - (pixelPosition.y));

            // The following centers the font.
            // HACK (kasiu): The font "height" is not always correct given the font. Check when using different fonts.
            Vector2 tweak = new Vector2(splitName[i].Length * fontSize, fontSize * 2);
            tweak /= 2.0f;
            p -= tweak;
            GUI.Box(new Rect(p.x, p.y, fontSize * splitName[i].Length, fontSize * 2), splitName[i], style); //, style);
        }
    }
}
