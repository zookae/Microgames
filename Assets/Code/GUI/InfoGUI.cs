using UnityEngine;
using System.Collections;


public enum GameStateInfo
{
    Tag,
    Round, // this is unfortunately not explicitly in game state
    Score,
}

/// <summary>
/// This should potentially, and eventually replace ScoreGUI
/// </summary>
public class InfoGUI : MonoBehaviour {

    /// <summary>
    /// Screen-space position for the uppermost item.
    /// </summary>
    public Vector2 origin;
    public Font font;
    public int fontSize;
    public Color fontColor;

    private GUIStyle textStyle;

    private string[] header;

	// Use this for initialization
	void Start () {
        textStyle = new GUIStyle();
        textStyle.font = font;
        textStyle.fontSize = fontSize;
        textStyle.normal.textColor = fontColor;
	}

    void OnGUI() {
        GUILayout.BeginArea(new Rect(origin.x, origin.y, Screen.width, Screen.height));
        GUILayout.BeginVertical(); //origin.x, origin.y);
        if (GameState.Singleton.labelTags.Count > 0)
        {
            GUILayout.Label("Your Location: " + GameState.Singleton.labelTags[0], textStyle);
        }
        GUILayout.Label("Round: " + (GameRoundCounter.GetCurrentRound() + 1), textStyle);
        GUILayout.Label("Score: " + GameState.Singleton.score, textStyle);
        GUILayout.EndVertical();
        GUILayout.EndArea();
    }
	
	// Update is called once per frame
	void Update () {
        // Check to see if any items need to be rendered	
	}
}
