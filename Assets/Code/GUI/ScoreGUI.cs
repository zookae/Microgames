using UnityEngine;
using System.Collections;

public class ScoreGUI : MonoBehaviour {

    public float xPos = Screen.width-100;
    public float yPos = 100;
    public float xSize = 100;
    public float ySize = 25;

    public int fontSize;
    public Color fontColor;

    /// <summary>
    /// Draws score on screen
    /// </summary>
    void OnGUI() {
        //Debug.Log("[ScoreGUI] score is: " + GameState.Singleton.score);
        GUIStyle style = new GUIStyle();
        style.fontSize = fontSize;
        style.normal.textColor = fontColor;

        GUI.Box(new Rect(xPos, yPos, 100, 25), "score: " + GameState.Singleton.score, style);        
    }
}
