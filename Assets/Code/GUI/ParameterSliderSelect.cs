using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// TODO: generalize so not specific to shooting behavior

public class ParameterSliderSelect : ParameterSlider {

    public List<MonoBehaviour> paramArray = new List<MonoBehaviour>();

    public string entity = "Enemy";

    private float newValue;
    public ParamType ptype;

    void Awake() {
        newValue = (paramMax - paramMin) / 2;
        Debug.Log("[ParameterSliderSelect] initializing value: " + newValue);

        GameObject[] objs = GameObject.FindGameObjectsWithTag(entity);
        foreach (GameObject o in objs) {
            if (ptype == ParamType.BULLET_SIZE ||
                ptype == ParamType.BULLET_SPEED ||
                ptype == ParamType.FIRERATE) {
                    paramArray.Add(o.GetComponent<Shoot>());
            } else if (
                ptype == ParamType.MOVE_DRAG ||
                ptype == ParamType.MOVE_FORCE) {
                    paramArray.Add(o.GetComponent<MoveByKeyForce>());
            }
        }
    }

    void OnGUI() {
        newValue = LabelSlider(new Rect(xPos, yPos, xSize, ySize), newValue, fontSize);

        foreach (MonoBehaviour p in paramArray) {
            if (p == null)
                continue;
            SetParameter(ptype, p, newValue);
        }
    }

}
