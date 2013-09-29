using UnityEngine;
using System.Collections;

public class ParameterSliderForce : ParameterSlider {

    public MoveByKeyForce param;

    void Awake() {
        param = GameObject.Find("Player").GetComponent<MoveByKeyForce>();
    }

    void OnGUI() {
        param.force = LabelSlider(new Rect(25, 55, 100, 30), param.force, 15);
    }
}
