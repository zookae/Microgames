using UnityEngine;
using System.Collections;

public class ScoreGUI : MonoBehaviour {

    public float xPos = Screen.width-100;
    public float yPos = 100;
    public float xSize = 100;
    public float ySize = 100;

    public int fontSize;

    /// <summary>
    /// Draws score on screen
    /// </summary>
    void OnGUI() {
        GUIStyle style = new GUIStyle();
        style.fontSize = fontSize;

        GUI.Box(new Rect(xPos, yPos, xSize, ySize), 
            "score : " + GameState.Singleton.score, style);
    }
}
