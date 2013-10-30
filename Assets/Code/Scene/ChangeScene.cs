using UnityEngine;
using System.Collections;

/// <summary>
/// This is currently duplicate functionality with SceneChange in GUI, which is currently hard-coded to Bullethell.
/// Will eventually generalize to multiple GUI events.
/// </summary>
public class ChangeScene : MonoBehaviour {
    public string sceneName;

    void OnMouseDown() {
        if (sceneName != null) {
            Application.LoadLevel(sceneName);
        }
    }
}
