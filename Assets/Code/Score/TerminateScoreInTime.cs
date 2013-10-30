using UnityEngine;
using System.Collections;

public class TerminateScoreInTime : Terminate {

    public float MaxTime;

	void Update () {
        if (PassedTime(MaxTime)) {
            GameState.Singleton.CurrentState = State.Lose; // lose if you take too long
        }
        else if (PassedThresh(GameState.Singleton.score, valueThreshold, ThresholdType.Above)) {
            GameState.Singleton.CurrentState = State.Win; // win if you ever exceed minimum score
        }
	}
}
