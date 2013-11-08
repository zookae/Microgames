using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DBGameStateManager : MonoBehaviour {
    /// <summary>
    /// The possible prefabs that can be spawned
    /// </summary>
    public GameObject[] prefabs;

    private bool hasReceivedMode;
    private bool hasReceivedTrace;
    private bool hasReceivedTags;
    private bool hasReceivedObjects;
    private bool sentTraceToDB;

    void Reset() {
        hasReceivedMode = false;
        hasReceivedTrace = false;
        hasReceivedTags = false;
        hasReceivedObjects = false;
        sentTraceToDB = false;

        // Request information from the server to get started.
        DebugConsole.Log("We're prodding the server for info.");
        NetworkClient.Instance.SendServerMess(NetworkClient.MessType_ToServer.SNGRequestNewGame, "");
        NetworkClient.Instance.SendServerMess(NetworkClient.MessType_ToServer.SNGRequestTrace, "");
    }

    // TODO (kasiu): Use this to replace LoadProxyGame's setup logic
    void Start() {
        //Reset();
    }

    public bool IsReadyToRun() {
        return hasReceivedMode && hasReceivedTrace && hasReceivedTags && hasReceivedObjects;
    }
   
    public bool SetMode(int mode) {
        if (hasReceivedMode) {
            return false;
        }
        GameObject spawner = GameObject.Find("Spawner");
        if (spawner != null && (spawner.GetComponent<LoadObject>() != null)) {
            switch (mode) {
                // 1-indexing to be consistent with the DB.
                case 1:
                    GameState.Singleton.ScoringMode = ScoringMode.Collaborative;
                    spawner.GetComponent<LoadObject>().prefab = prefabs[0];
                    break;
                case 2:
                    GameState.Singleton.ScoringMode = ScoringMode.Competitive;
                    spawner.GetComponent<LoadObject>().prefab = prefabs[0];
                    break;
                case 3:
                    GameState.Singleton.ScoringMode = ScoringMode.Both;
                    spawner.GetComponent<LoadObject>().prefab = prefabs[1];
                    break;
                default:
                    return false;
            }
            hasReceivedMode = true;
            return true;
        }
        return false;
    }

    public bool SetPartnerTrace(List<Triple<double, string, string>> trace) {
        if (hasReceivedTrace) {
            return false;
        }
        GameState.Singleton.partnerTrace = trace;
        hasReceivedTrace = true;
        return true;
    }

    public bool SetObjectList(List<string> objects) {
        if (hasReceivedObjects) {
            return false;
        }

        // Modify the spawner
        GameObject spawner = GameObject.Find("Spawner");
        if (spawner != null && (spawner.GetComponent<LoadObject>() != null)) {
            spawner.GetComponent<LoadObject>().objectNames = objects;
            hasReceivedObjects = true;
            return true;
        }
        return false;
    }

    public bool SetTagList(List<string> tags) {
        if (hasReceivedObjects) {
            return false;
        }

        // Modify the spawner
        GameObject spawner = GameObject.Find("Spawner");
        if (spawner != null && (spawner.GetComponent<LoadObject>() != null)) {
            spawner.GetComponent<LoadObject>().tagNames = tags;
            hasReceivedObjects = true;
            return true;
        }
        return false;
    }
	
	// Update is called once per frame
	void Update () {
        //if (!IsReadyToRun()) {
        //    GameState.Singleton.CurrentState = State.Paused;
        //}

        if (!sentTraceToDB && (GameState.Singleton.CurrentState == State.Win ||
            GameState.Singleton.CurrentState == State.Lose)) {
            // HACK (kasiu): Currently using a comma-delimeted thingy.
            NetworkClient.Instance.SendServerMess(NetworkClient.MessType_ToServer.SNGSavePlayerData, "");
            NetworkClient.Instance.SendServerMess(NetworkClient.MessType_ToServer.SNGSaveDBTrace, DBStringHelper.traceToString(GameState.Singleton.clickTrace, ':'));
            sentTraceToDB = true;
        }
	}
}


