using UnityEngine;
using System.Collections;

public class ProgressBar : MonoBehaviour {
	
	public float barDisplay;

    public float maxTime;
	
	Vector2 pos = new Vector2(Screen.width/2,Screen.height-100);
	Vector2 size = new Vector2(100,20);
	
	public Texture2D progressBarEmpty;
	public Texture2D progressBarFull;
	
	void OnGUI() {
		/*
		// create a GUI group @ width of bar and 2x height for room for text
		GUI.BeginGroup(new Rect(10,10, barWidth, barHeight*2));
		
		// draw box for back of progress bar, no text inside
		GUI.Box(new Rect(0,0, barWidth, barHeight), "");
		
		// make label to draw the progress icon texture
		// use barProgress to set its X, 0 as Y, and width/height of the texture used
		GUI.Label(new Rect(barProgress, 0, progIcon.width, progIcon.height), progIcon);
		
		GUI.EndGroup();
		*/
		// draw the background:
    	GUI.BeginGroup (new Rect (pos.x, pos.y, size.x, size.y));
        GUI.Box(new Rect (0,0, size.x, size.y),progressBarEmpty);
 
        // draw the filled-in part:
        GUI.BeginGroup(new Rect (0, 0, size.x * barDisplay, size.y));
            GUI.Box(new Rect (0,0, size.x, size.y),progressBarFull);
        GUI.EndGroup ();
 
    	GUI.EndGroup ();
	}
	
	void Update() {
        if (GameState.Singleton.CurrentState == State.Running) {
            //barDisplay = GameState.Singleton.TimeUsed / Termination.maxTime;
            barDisplay = GameState.Singleton.TimeUsed / maxTime;
        }
	}
}
