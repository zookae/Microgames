using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParameterSliderShootRate : ParameterSlider {

    public List<NPCShootInDirection> paramArray;

    public string entity = "Enemy";

    private float newValue;

    void Awake() {
        newValue = (paramMax - paramMin) / 2;

        GameObject[] objs = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject o in objs) {
            paramArray.Add(o.GetComponent<NPCShootInDirection>());
        }
    }

    void OnGUI() {
        newValue = LabelSlider(new Rect(25, 115, 100, 30), newValue, 15);

        foreach (NPCShootInDirection p in paramArray) {
            p.frequency = newValue;
        }
    }
}
