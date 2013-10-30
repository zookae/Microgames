using UnityEngine;
using System.Collections;

public class TerminateTime : Terminate {

    // Update is called once per frame
	void Update () {
        if (PassedTime(valueThreshold)) {
            GameState.Singleton.CurrentState = termState;
        }
	}
}
