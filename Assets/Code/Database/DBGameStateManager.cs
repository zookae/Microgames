using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DBGameStateManager : MonoBehaviour {
    /// <summary>
    /// The possible prefabs that can be spawned
    /// </summary>
    public GameObject[] prefabs;

    public TextAsset mappingAsset;

    /// <summary>
    /// The spawner prefab
    /// </summary>
    public GameObject spawner;

    private bool hasReceivedMode;
    private bool hasReceivedTrace;
    private bool hasReceivedTags;
    private bool hasReceivedObjects;
    private bool hasSpawnedObjects;
    private bool sentTraceToDB;

    // Information received from the server...
    private ScoringMode mode;
    private List<string> tagList;
    private List<string> objectList;
    private List<Triple<double, string, string>> opponentTrace;

    void Reset() {
        hasReceivedMode = false;
        hasReceivedTrace = false;
        hasReceivedTags = false;
        hasReceivedObjects = false;
        sentTraceToDB = false;

        // Request information from the server to get started.
        // XXX(kasiu) REMOVE THE ELSE WHEN WE DEPLOY PLEASE.
        if (Network.isClient) {
            DebugConsole.Log("We're prodding the server for info.");
            NetworkClient.Instance.SendServerMess(NetworkClient.MessType_ToServer.SNGRequestNewGame, "");
            NetworkClient.Instance.SendServerMess(NetworkClient.MessType_ToServer.SNGRequestTrace, "");
        } else {
            // Load a proxy so we can debug things.
            SetMode(ProxyGameGenerator.SelectRandomScoringMode());
            SetObjectList(ProxyGameGenerator.SelectRandomObjectSet(7));
            SetTagList(ProxyGameGenerator.SelectRandomTagSet());
            SetPartnerTrace(ProxyGameGenerator.SelectRandomPartnerTrace(objectList, tagList, 0.0f, GameState.Singleton.MaxTime));
        }
    }

    // TODO (kasiu): Use this to replace LoadProxyGame's setup logic
    void Start() {
        Reset();
    }

    public bool IsReadyToSpawn() {
        return hasReceivedMode && hasReceivedTrace && hasReceivedTags && hasReceivedObjects;
    }
   
    public bool SetMode(int m) {
        if (hasReceivedMode) {
            return false;
        }
        switch (m) {
            // 1-indexing to be consistent with the DB.
            case 1:
                GameState.Singleton.ScoringMode = ScoringMode.Collaborative;
                this.mode = ScoringMode.Collaborative;
                break;
            case 2:
                GameState.Singleton.ScoringMode = ScoringMode.Competitive;
                this.mode = ScoringMode.Competitive;
                break;
            case 3:
                GameState.Singleton.ScoringMode = ScoringMode.Both;
                this.mode = ScoringMode.Both;
                break;
            default:
                return false;
        }
        hasReceivedMode = true;
        return true;
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
        objectList = objects;
        hasReceivedObjects = true;
        return true;
    }

    public bool SetTagList(List<string> tags) {
        if (hasReceivedTags) {
            return false;
        }

        tagList = tags;
        hasReceivedTags = true;
        return true;
    }

    void OnGUI() {
        if (!hasSpawnedObjects && !IsReadyToSpawn()) {
            // TODO (kasiu): Display a loading message.
            DebugConsole.Log("Not ready to spawn objects!");
            DebugConsole.Log("has trace " + hasReceivedTrace);
            DebugConsole.Log("has objects " + hasReceivedObjects);
            DebugConsole.Log("has tags " + hasReceivedTags);
            DebugConsole.Log("has mode " + hasReceivedMode);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (!hasSpawnedObjects && IsReadyToSpawn()) {
            InstantiateSpawner();
        }

        if (!sentTraceToDB && (GameState.Singleton.CurrentState == State.Win ||
            GameState.Singleton.CurrentState == State.Lose)) {
            // HACK (kasiu): Currently using a comma-delimeted thingy.
            // NetworkClient.Instance.SendServerMess(NetworkClient.MessType_ToServer.SNGSavePlayerData, "");
            NetworkClient.Instance.SendServerMess(NetworkClient.MessType_ToServer.SNGSaveDBTrace, DBStringHelper.traceToString(GameState.Singleton.clickTrace, ':'));
            NetworkClient.Instance.SendServerMess(NetworkClient.MessType_ToServer.SNGSavePlayerScore, ((int)(GameState.Singleton.score)).ToString());
            sentTraceToDB = true;
        }
	}

    private void InstantiateSpawner() {
        DebugConsole.Log("Instantiating spawner!");
        // SPAWN THE SPAWNER.
        GameObject newSpawner = (GameObject)GameObject.Instantiate(spawner, spawner.transform.position, spawner.transform.rotation);
        newSpawner.AddComponent<LoadObject>();
        newSpawner.GetComponent<LoadObject>().objectNames = objectList;
        newSpawner.GetComponent<LoadObject>().tagNames = tagList;
        if (mode == ScoringMode.Both) {
            newSpawner.GetComponent<LoadObject>().prefab = prefabs[1];
        } else {
            newSpawner.GetComponent<LoadObject>().prefab = prefabs[0];
        }
        newSpawner.GetComponent<LoadObject>().objectSpriteMapping = mappingAsset;
        hasSpawnedObjects = true;
    }
}


