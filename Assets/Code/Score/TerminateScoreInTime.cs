using UnityEngine;
using System.Collections;

public class TerminateScoreInTime : Terminate {

	void Update () {
        if (PassedTime()) {
            GameState.Singleton.CurrentState = State.Lose; // lose if you take too long
        }
        else if (PassedThresh(GameState.Singleton.score, valueThreshold, ThresholdType.Above)) {
            GameState.Singleton.CurrentState = State.Win; // win if you ever exceed minimum score
        }
	}
}
