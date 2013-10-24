using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DBSend {

    // Use this for initialization
    public static void Score(float score) {
        DebugConsole.Log("[DBSendScore] sending score");
        NetworkClient.Instance.SendServerMess(NetworkClient.MessType_ToServer.SaveDBStr, score.ToString()); //not a blocking call
    }

    public static void ActionTrace(List<ParamChange> actionTrace) {
        DebugConsole.Log("[DBSendScore] sending actions");
        foreach (ParamChange action in actionTrace) {
            NetworkClient.Instance.SendServerMess(NetworkClient.MessType_ToServer.SaveDBStr, action.ToString()); //not a blocking call
        }
        
    }
}
