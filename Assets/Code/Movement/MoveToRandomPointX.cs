using UnityEngine;
using System.Collections;

public class MoveToRandomPointX : MoveRandomPoint {

	// Use this for initialization
	void Start () {
        movementPoint = RandomPoint(moveBounds);
        movementPoint.y = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
        // if close enough to point pick a new one
        if (Vector3.Distance(transform.position, movementPoint) < minDist) {
            movementPoint = RandomPoint(moveBounds);
            movementPoint.y = transform.position.y;
        }
        MoveToPointM(movementPoint);
	}
}
