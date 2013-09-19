using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Possible states for the game to be in
/// </summary>
public enum State {
    Running,
    Win,
    Lose
}

public enum Resource {
    NumDestoyed,
    Score
}

public class GameState : MonoBehaviour {


    /// <summary>
    /// The time the game has run so far.
    /// </summary>
    public float TimeUsed = 0.0f;

    /// <summary>
    /// Alternative states for game to be in
    /// </summary>
    public State CurrentState = State.Running;

    /// <summary>
    /// Global score
    /// </summary>
    public float score = 0.0f;

    /// <summary>
    /// Table of resource amounts
    /// </summary>
    public Dictionary<Resource, float> resources = new Dictionary<Resource, float>();

    /// <summary>
    /// Track resources amounts by owner of resource
    /// </summary>
    //public Dictionary<int, Dictionary<Resource, float>> resourceOwners = new Dictionary<int, Dictionary<Resource, float>>();

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
	
	// Use this for initialization
	void Start () {
        resources[Resource.NumDestoyed] = 0;
        resources[Resource.Score] = 0;
	}
	
	// Update is called once per frame
	void Update () {
        TimeUsed += Time.deltaTime;
	}
}
