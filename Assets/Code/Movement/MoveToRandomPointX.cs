using UnityEngine;
using System.Collections;

public class MoveToRandomPointX : MoveRandomPoint {

	// Use this for initialization
	void Start () {
        movementPoint = RandomPointX(moveBounds);
	}
	
	// Update is called once per frame
	void Update () {
        // if close enough to point (along X axis) pick a new one
        if (Mathf.Abs(transform.position.x - movementPoint.x) < minDist) {
            movementPoint = RandomPointX(moveBounds);
        }
        MoveToPointXM(movementPoint);
	}
}
