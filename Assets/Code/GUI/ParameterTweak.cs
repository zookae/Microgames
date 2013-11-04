using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public class ParameterTweak<T> : MonoBehaviour where T : Component {

    public MechanicTweaker<T> tweak;

    void Awake() {
        tweak = gameObject.GetComponent<MechanicTweaker<T>>();
    }

    void OnGUI() {

        if (GUI.Button(new Rect(10, 10, 100, 50), "set parameter")) {
            tweak.setTargetField();
        }
        if (GUI.Button(new Rect(10, 60, 100, 50), "tweak me!")) {
            tweak.tweak();
        }
    }

}
