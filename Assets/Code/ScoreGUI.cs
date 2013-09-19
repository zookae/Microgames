using UnityEngine;
using System.Collections;

public class ScoreGUI : MonoBehaviour {

    /// <summary>
    /// Draws score on screen
    /// </summary>
    void OnGUI() {
        GUI.Box(new Rect(10, 10, 100, 20), "score : " + GameState.Singleton.score);
    }
}
