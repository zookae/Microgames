using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DBGameStateManager : MonoBehaviour {

    private bool hasLoaded;
    private bool sentTraceToDB;

	// Use this for initialization
	void Start () {
	}

    // TODO (kasiu): Use this to replace LoadProxyGame's setup logic
    void Awake() {
        hasLoaded = false;
        sentTraceToDB = false;

        // Request information from the server to get started.
        // NetworkClient.Instance.SendServerMess(NetworkClient.MessType_ToServer.SNGRequestTrace, "");
    }
	
	// Update is called once per frame
	void Update () {
        if (!sentTraceToDB && (GameState.Singleton.CurrentState == State.Win ||
            GameState.Singleton.CurrentState == State.Lose)) {
            // HACK (kasiu): Currently using a comma-delimeted thingy.
            NetworkClient.Instance.SendServerMess(NetworkClient.MessType_ToServer.SNGSaveDBTrace, DBStringHelper.traceToString(GameState.Singleton.clickTrace, ':'));
            sentTraceToDB = true;
        }
	}
}


