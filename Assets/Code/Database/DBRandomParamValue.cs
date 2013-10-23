using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DBRandomParamValue : ParameterSlider {

    public List<Shoot> paramArray = new List<Shoot>();

    public string entity = "Enemy";

    private float randomValue;
    public ParamType ptype;

    void Awake() {
        randomValue = Random.Range(0.0f, 1.0f); // generate random value
        float newValue = (paramMax - paramMin) * randomValue; // set to value in scaling range

        GameObject[] objs = GameObject.FindGameObjectsWithTag(entity);
        foreach (GameObject o in objs) {
            paramArray.Add(o.GetComponent<Shoot>());
        }

        Debug.Log("setting " + ptype.ToString() + " to value: " + newValue);

        foreach (Shoot p in paramArray) {
            if (p == null)
                continue;
            SetParameter(ptype, p, newValue);
        }
    }

}
