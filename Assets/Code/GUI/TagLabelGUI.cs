using UnityEngine;
using System.Collections;

public class TagLabelGUI : MonoBehaviour {
    /// <summary>
    /// Text size.
    /// </summary>
    public int fontSize = 10;

    public float x, y;

    /// <summary>
    /// If the tag name contains spaces, we store the individual words.
    /// </summary>
    public string[] splitName;

	// Use this for initialization
	void Start () {
        splitName = this.gameObject.name.Split(' ');
	}

    /// <summary>
    /// Draws score on screen
    /// </summary>
    void OnGUI() {
        GUIStyle style = new GUIStyle();
        style.fontSize = fontSize;

        Vector3 pixelPosition = Camera.main.WorldToScreenPoint(this.gameObject.transform.position);
        // NOTE (kasiu): Screen.height - pixelPosition.y sets the y position correctly because FOO SCREENSPACE axes reversal :(
        // TODO (kasiu): YES, IT'S LEFT-JUSTIFIED FONT. DEAL WITH IT.
        for (var i = 0; i < splitName.Length; i++) {
            GUI.Box(new Rect(pixelPosition.x, Screen.height + (fontSize * i) - (pixelPosition.y), 100, 100), splitName[i], style);
        }
    }
}
