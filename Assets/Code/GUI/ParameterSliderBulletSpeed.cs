using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParameterSliderBulletSpeed : ParameterSlider {

    public List<Shoot> paramArray = new List<Shoot>();

    public string entity = "Enemy";

    private float newValue;

    void Awake() {
        newValue = (paramMax - paramMin) / 2;

        GameObject[] objs = GameObject.FindGameObjectsWithTag(entity);
        foreach (GameObject o in objs) {
            paramArray.Add(o.GetComponent<Shoot>());
        }
    }

    void OnGUI() {
        newValue = LabelSlider(new Rect(25, 25, 100, 30), newValue, 15);

        foreach (Shoot p in paramArray) {
            p.moveSpeed = newValue;
        }
    }
}
