using UnityEngine;
using System.Collections;

public class ParameterSliderDrag : ParameterSlider {

    public MoveByKeyForce param;

    void Awake() {
        param = GameObject.Find("Player").GetComponent<MoveByKeyForce>();
    }

    void OnGUI() {
        param.drag = LabelSlider(new Rect(xPos, yPos, xSize, ySize), param.drag, fontSize);
    }
}
