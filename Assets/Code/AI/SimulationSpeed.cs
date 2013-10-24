using UnityEngine;
using System.Collections;

public class SimulationSpeed : MonoBehaviour {

    public float newTimeScale = 1.0f;

	// Use this for initialization
	void Awake() {
        Time.timeScale = newTimeScale;
	}

}
