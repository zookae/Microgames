using UnityEngine;
using System.Collections;

/// <summary>
/// Possible states for the game to be in
/// </summary>
public enum State {
    Running,
    Win,
    Lose
}

public class GameState : MonoBehaviour {
    /// <summary>
    /// Alternative states for game to be in
    /// </summary>
    public State CurrentState = State.Running;

    static GameState singleton;

    public static GameState Singleton {
        get {
            if (singleton == null) {
                singleton = FindObjectOfType(typeof(GameState)) as GameState;

                if (singleton != null) {
                    return singleton;
                }

                GameObject client = new GameObject("Global Game State");
                singleton = client.AddComponent<GameState>();
                singleton.Start();
            }

            return singleton;
        }
    }
	
	/// <summary>
	/// The time the game has run so far.
	/// </summary>
	public float TimeUsed = 0.0f;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        TimeUsed += Time.deltaTime;
	}
}
