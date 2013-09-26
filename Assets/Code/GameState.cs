﻿using UnityEngine;
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
    /// Maximum length for the game to run.
    /// </summary>
    public float MaxTime = 7.0f;

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
    /// Record of history of objects clicked on by player
    /// </summary>
    public List<Triple<double, string, string>> clickTrace = new List<Triple<double,string,string>>();

    /// <summary>
    /// Record of history of objects clicked on by partner
    /// </summary>
    public List<Triple<double, string, string>> partnerTrace = new List<Triple<double, string, string>>();

    /// <summary>
    /// Tags this game instance uses for blocking
    /// </summary>
    public List<string> blockTags = new List<string>();

    /// <summary>
    /// Tags this game instance uses for labeling
    /// </summary>
    public List<string> labelTags = new List<string>();

    /// <summary>
    /// Track resources amounts by owner of resource
    /// </summary>
    //public Dictionary<int, Dictionary<Resource, float>> resourceOwners = new Dictionary<int, Dictionary<Resource, float>>();


    // cf: http://clearcutgames.net/home/?p=437
    // (v1) Allow manipulation in editor and prevent duplicates
    // static singleton property
    public static GameState Singleton { get; private set; }

    // instantiate on game start
    void Awake() {

        // check for conflicting instances
        if (Singleton != null && Singleton != this) {
            Destroy(gameObject); // destroy others that conflict
        }

        Singleton = this; // save singleton instance

        DontDestroyOnLoad(gameObject); // ensure not destroyed b/t scenes
    }

    // cf: http://clearcutgames.net/home/?p=437
    // (v2) Instantiate lazily
    //public static GameState singleton;
    //public static GameState Singleton {
    //    get { return singleton ?? (singleton = new GameObject("GlobalState").AddComponent<GameState>()); }
    //}
	
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
