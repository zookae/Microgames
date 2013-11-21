using UnityEngine;
using System.Collections;

public class SceneChangeGUI : MonoBehaviour {
	
	public string NextScene;
	
    void OnGUI() {
        if (GUI.Button(new Rect(Screen.width-100, Screen.height-70, 100, 30), "Ready!")) {
            Debug.Log("clicked to finish parameters");
			string traceStr = "";
			foreach (ParamChange tstr in GameState.Singleton.actionTrace) {
				traceStr += tstr + ";";
			}
			Debug.Log("[SceneChangeGUI] sending trace string: " + traceStr);
			NetworkClient.Instance.SendServerMess(NetworkClient.MessType_ToServer.BHSaveParamTrace, traceStr);
            Application.LoadLevel(NextScene);
        }
    }
}
