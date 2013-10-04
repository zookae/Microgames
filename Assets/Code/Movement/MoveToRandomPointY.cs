using UnityEngine;
using System.Collections;

public class MoveToRandomPointY : MoveRandomPoint {

    // Use this for initialization
    void Start() {
        movementPoint = RandomPointY(moveBounds);
    }

    // Update is called once per frame
    void Update() {
        // if close enough to point (along X axis) pick a new one
        if (Mathf.Abs(transform.position.y - movementPoint.y) < minDist) {
            movementPoint = RandomPointY(moveBounds);
        }
        MoveToPointYM(movementPoint);
    }
}
