using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParameterSliderShootRate : ParameterSlider {

    public List<NPCShootInDirection> paramArray;

    public string entity = "Enemy";

    private float newValue;

    void Awake() {
        newValue = (paramMax - paramMin) / 2;

        GameObject[] objs = GameObject.FindGameObjectsWithTag(entity);
        foreach (GameObject o in objs) {
            paramArray.Add(o.GetComponent<NPCShootInDirection>());
        }
    }

    void OnGUI() {
        newValue = LabelSlider(new Rect(xPos, yPos, xSize, ySize), newValue, fontSize);

        foreach (NPCShootInDirection p in paramArray) {
            p.gameObject.GetComponent<NPCShootInDirection>().frequency = newValue;
        }
    }
}
