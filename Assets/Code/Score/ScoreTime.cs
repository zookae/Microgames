using UnityEngine;
using System.Collections;

public class ScoreTime : MonoBehaviour {

    /// <summary>
    /// Frequency (seconds) for applying score change
    /// </summary>
    public float scoreFrequency;

    /// <summary>
    /// Amount of points to award at each interval
    /// </summary>
    public float pointChange;

    private float timeDelta;

    // Use this for initialization
    void Start() {
        timeDelta = 0.0f;
    }

    // Update is called once per frame
    void Update() {
        timeDelta += Time.deltaTime;

        if (timeDelta > scoreFrequency) {
            GameState.Singleton.score += pointChange;
            timeDelta = 0.0f;
        }
    }
}
