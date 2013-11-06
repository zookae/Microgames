using UnityEngine;
using System.Collections;

public class DBGameStateManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}

    // TODO (kasiu): Use this to replace LoadProxyGame's setup logic
    void Awake() {
        // TODO (kasiu): Need to write a string parser.
    }
	
	// Update is called once per frame
	void Update () {
        if (GameState.Singleton.CurrentState == State.Win ||
            GameState.Singleton.CurrentState == State.Lose) {
            // Send trace to the database if it hasn't already.
        }
	}
}
