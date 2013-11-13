using UnityEngine;
using System.Collections;

public class ProgressBarV2 : MonoBehaviour {

    public float barDisplay;

    public Vector2 position;
    public Vector2 size;

    public Color progressBarEmpty;
    public Color progressBarFull;

    public float maxtime;

    private Texture2D emptyStyle;
    private Texture2D fullStyle;

    void Start() {
        position = (position == null) ? new Vector2(Screen.width / 2, Screen.height - 100) : position;
        size = (size == null) ? new Vector2(100, 20) : size;

        emptyStyle = GUIUtils.MakeBlankTexture((int)size.x, (int)size.y, progressBarEmpty);
        fullStyle = GUIUtils.MakeBlankTexture((int)size.x, (int)size.y, progressBarFull);
    }

    void OnGUI() {
        //GUILayout.BeginArea(new Rect(position.x, position.y, size.x, size.y));

        //GUILayout.Box(emptyStyle);
        //GUILayout.Box(fullStyle);
        //GUILayout.EndArea();


        GUI.BeginGroup(new Rect(position.x, position.y, size.x, size.y));
        GUI.Box(new Rect(0, 0, size.x, size.y), emptyStyle);

        // draw the filled-in part:
        GUI.BeginGroup(new Rect(0, 0, size.x * barDisplay, size.y));
        GUI.Box(new Rect(0, 0, size.x, size.y), fullStyle);
        GUI.EndGroup();

        GUI.EndGroup();
    }

    void Update() {
        if (GameState.Singleton.CurrentState == State.Running) {
            barDisplay = GameState.Singleton.TimeUsed / this.maxtime;
        }
    }
    
}