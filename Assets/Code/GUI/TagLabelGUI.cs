using UnityEngine;
using System.Collections;

public class TagLabelGUI : MonoBehaviour {
    /// <summary>
    /// Text size.
    /// </summary>
    public int fontSize = 10;

    public float x, y;

	// Use this for initialization
	void Start () {
	}

    /// <summary>
    /// Draws score on screen
    /// </summary>
    void OnGUI() {
        GUIStyle style = new GUIStyle();
        style.fontSize = fontSize;

        Vector3 pixelPosition = Camera.main.WorldToScreenPoint(this.gameObject.transform.position);
        // NOTE (kasiu): Screen.height - pixelPosition.y sets the y position correctly because FOO SCREENSPACE axes reversal :(
        GUI.Box(new Rect(pixelPosition.x, Screen.height - pixelPosition.y, 100, 100), this.gameObject.name, style);
    }
}
