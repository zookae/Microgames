using UnityEngine;
using System.Collections;

public class SceneChangeGUI : MonoBehaviour {

    void OnGUI() {
        if (GUI.Button(new Rect(Screen.width-100, Screen.height-70, 100, 30), "Ready!")) {
            Debug.Log("clicked to finish parameters");
            Application.LoadLevel("Bullethell");
        }
    }
}
