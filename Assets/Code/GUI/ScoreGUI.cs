using UnityEngine;
using System.Collections;

public class ScoreGUI : MonoBehaviour {

    public float xPos = Screen.width-100;
    public float yPos = 100;
    public float xSize = 100;
    public float ySize = 100;

    public int fontSize;
    public Color fontColor;

    /// <summary>
    /// Draws score on screen
    /// </summary>
    void OnGUI() {
        GUIStyle style = new GUIStyle();
        style.fontSize = fontSize;
        style.normal.textColor = fontColor;
        //style.normal.textColor = Color.yellow;

        GUI.Box(new Rect(xPos, yPos, xSize, ySize), 
            "score : " + GameState.Singleton.score, style);
    }
}
