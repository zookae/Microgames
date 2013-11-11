using UnityEngine;
using System.Collections;

/// <summary>
/// Used to maintain database calls across rounds/levels/etc. Kept separate from DBGameStateManager.
/// </summary>
public class DBPlayerStateManager : MonoBehaviour {

    private bool setupPlayerData = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (!setupPlayerData && Network.isClient == true) {
            // Builds the initial player data. THIS IS USEFUL AND IMPORTANT.
            // NOTE (kasiu): There's some weird asynchronous bugs if I stick this in DBGameStateManager.
            DebugConsole.Log("SETUP PLAYER DATA!");
            NetworkClient.Instance.SendServerMess(NetworkClient.MessType_ToServer.SNGSavePlayerData, "");
            setupPlayerData = true;
        }
	}
}
