using UnityEngine;
using System.Collections;

public class ParameterSliderDrag : ParameterSlider {

    public MoveByKeyForce param;

    void Awake() {
        param = GameObject.Find("Player").GetComponent<MoveByKeyForce>();
    }

    void OnGUI() {
        param.drag = LabelSlider(new Rect(25, 155, 100, 30), param.drag, 15);
    }
}
