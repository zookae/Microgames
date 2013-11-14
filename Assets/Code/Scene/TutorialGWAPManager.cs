using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TutorialGWAPManager : MonoBehaviour {
    /// <summary>
    /// The possible prefabs that can be spawned
    /// </summary>
    public GameObject[] prefabs;

    public TextAsset mappingAsset;

    /// <summary>
    /// The spawner prefab
    /// </summary>
    public GameObject spawner;

    // Information received from the server...
    private ScoringMode mode;
    private List<string> tagList;
    private List<string> objectList;
    private List<Triple<double, string, string>> opponentTrace;

    private bool hasReceivedMode;
    private bool hasReceivedTrace;
    private bool hasReceivedTags;
    private bool hasReceivedObjects;
    private bool hasSpawnedObjects;

    public void Reset() {
        hasReceivedMode = false;
        hasReceivedTrace = false;
        hasReceivedTags = false;
        hasReceivedObjects = false;

        // Request information from the server to get started.
        // XXX(kasiu) REMOVE THE ELSE WHEN WE DEPLOY PLEASE.
        if (Network.isClient) {
            DebugConsole.Log("We're prodding the server for info.");
            NetworkClient.Instance.SendServerMess(NetworkClient.MessType_ToServer.SNGRequestGameMode,"");
        } else {
            // Load a proxy so we can debug things in the editor.
            Debug.Log("We're loading a proxy game because we're probably in the editor.");
            SetMode(ProxyGameGenerator.SelectRandomScoringMode());
        }
        SetObjectList();
    }

	// Use this for initialization
	void Start () {
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
                return false; // WE SHOULD NEVER GET HERE
        }
        hasReceivedMode = true;

        // We got the tags before we got the mode, so we need to go back and resolve it.
        if (hasReceivedTags) {
            ResolveTags();
        }
        return true;
    }

    private bool SetPartnerTrace(float minTime, float maxTime) {
        if (hasReceivedTrace) {
            return false;
        }
        if (!hasReceivedTags || !hasReceivedMode || !hasReceivedObjects) {
            return false;
        }

        opponentTrace = new List<Triple<double, string, string>>();

        switch (mode) {
            case ScoringMode.Collaborative:
            case ScoringMode.Competitive:
                opponentTrace.Add(new Triple<double, string, string>(0.0f, "Ice Cream", "Supermarket"));
                opponentTrace.Add(new Triple<double, string, string>(maxTime / 2.0, "Hammer", "Hardware Store"));
                opponentTrace.Add(new Triple<double, string, string>(maxTime - 1.0f, "Cabbage", "Supermarket"));
                break;
            case ScoringMode.Both:
                opponentTrace.Add(new Triple<double, string, string>(0.0f, "Ice Cream", "Supermarket"));
                opponentTrace.Add(new Triple<double, string, string>(maxTime / 2.0, "Hammer", "Hardware Store-compete"));
                opponentTrace.Add(new Triple<double, string, string>(maxTime - 1.0f,"Cabbage","Supermarket"));
                break;
            default:
                break; // WE SHOULD NEVER GET HERE
        }

        // BUILD ME
        GameState.Singleton.partnerTrace = opponentTrace;
        hasReceivedTrace = true;
        return true;
    }

    private bool SetObjectList() {
        if (hasReceivedObjects) {
            return false;
        }

        objectList = new List<string>();
        objectList.Add("Hammer");
        objectList.Add("Cabbage");
        objectList.Add("Ice Cream");
        hasReceivedObjects = true;
        return true;
    }

    public bool SetTagList() {
        if (hasReceivedTags) {
            return false;
        }

        tagList = new List<string>();
        tagList.Add("Hardware Store");
        tagList.Add("Supermarket");
        if (hasReceivedMode) {
            ResolveTags();
        }
        return true;
    }

    // XXX (kasiu): There is some incomplete logic in here that is based on how I choose to implement
    //              certain things. In particular, ScoringMode.Both is currently causing some problems.
    public void ResolveTags() {
        if (!hasReceivedMode) {
            return;
        }

        // Need to change taglist if we're using both
        if (mode == ScoringMode.Both) {
            string opponentTag = tagList[tagList.Count - 1];
            tagList[tagList.Count - 1] = opponentTag + "-compete";
            tagList.Add(opponentTag + "-collab");
        }
        hasReceivedTags = true;
    }

    void OnGUI() {
        if (!hasSpawnedObjects && !IsReadyToSpawn()) {
            // TODO (kasiu): Display a loading message.
            Vector2 size = new Vector2(200, 100);
            Vector2 pos = GUIUtils.ComputeCenteredPosition((int)size.x, (int)size.y);
            GUILayout.BeginArea(new Rect(pos.x, pos.y, size.x, size.y));
            GUILayout.Label("Generating game...");
            GUILayout.EndArea();

            DebugConsole.Log("Not ready to spawn objects!");
            DebugConsole.Log("has trace " + hasReceivedTrace);
            DebugConsole.Log("has objects " + hasReceivedObjects);
            DebugConsole.Log("has tags " + hasReceivedTags);
            DebugConsole.Log("has mode " + hasReceivedMode);
        }
    }


    // Update is called once per frame
    void Update() {
        // Set things.
        if (!hasReceivedTags) {
            SetTagList();
        }
        if (!hasReceivedTrace && hasReceivedObjects && hasReceivedTags && hasReceivedTags) {
            SetPartnerTrace(0.0f, GameState.Singleton.MaxTime);
        }

        if (!hasSpawnedObjects && IsReadyToSpawn()) {
            InstantiateSpawner();
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


