﻿using UnityEngine;
using System.Collections;

public class DestroyPrint : MonoBehaviour {

    void OnDestroy() {
        try {
            Debug.Log("total destroyed before: " + GameState.Singleton.resources[ResourceType.NumDestoyed]);
            Debug.Log(transform.GetInstanceID() + " was destroyed");
            GameState.Singleton.resources[ResourceType.NumDestoyed] = GameState.Singleton.resources[ResourceType.NumDestoyed] + 1;
            Debug.Log("total destroyed after: " + GameState.Singleton.resources[ResourceType.NumDestoyed]);
        }
        catch {
        }
    }
}
