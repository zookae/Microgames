using UnityEngine;
using System.Collections;

public class DBSendNameOnDestroy : MonoBehaviour {
    
    void OnDestroy() {
        DebugConsole.Log("[DBSendNameOnDestroy] sending object name");
        NetworkClient.Instance.SendServerMess(NetworkClient.MessType_ToServer.SaveDBStr, gameObject.name); //not a blocking call
        NetworkClient.Instance.SendServerMess(NetworkClient.MessType_ToServer.SaveDBStr, gameObject.GetInstanceID().ToString()); //not a blocking call
    }
}
