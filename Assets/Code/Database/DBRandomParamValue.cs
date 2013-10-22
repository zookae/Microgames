using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ParamType {
    BULLETSPEED,
    BULLETSIZE,
    FIRERATE
}

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
            switch (ptype) {
                case ParamType.BULLETSPEED:
                    p.bulletSpeed = newValue;
                    break;
                case ParamType.BULLETSIZE:
                    p.spawn.transform.localScale = new Vector3(newValue, newValue);
                    break;
                case ParamType.FIRERATE:
                    p.gameObject.GetComponent<NPCShootInDirection>().frequency = newValue;
                    break;
            }
            
        }
    }

}
