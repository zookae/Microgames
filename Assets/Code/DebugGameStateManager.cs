using UnityEngine;
using System.Collections;

// A script for handling debug UI interaction (e.g. pausing the game on the fly and things).
public class DebugGameStateManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        // Pauses/Unpauses the game
        if (Input.GetKeyUp(KeyCode.P) && GameState.Singleton.CurrentState != State.Paused) {
            GameState.Singleton.CurrentState = State.Paused;
        } else if (Input.GetKeyUp(KeyCode.P) && GameState.Singleton.CurrentState == State.Paused) {
            GameState.Singleton.CurrentState = State.Running;
        }
	}
}
