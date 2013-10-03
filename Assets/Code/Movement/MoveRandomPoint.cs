using UnityEngine;
using System.Collections;

public class MoveRandomPoint : MoveToPoint {

    /// <summary>
    /// Boundary on where movement can go
    /// </summary>
    public GameObject moveBounds;

    void Start() {
        movementPoint = RandomPoint(moveBounds);
    }

	// Update is called once per frame
	void Update () {
        //Debug.Log("[MoveRandomPoint]: moving to - " + movementPoint);
        
        // if close enough to point pick a new one
        if (Vector3.Distance(transform.position, movementPoint) < minDist) {
            movementPoint = RandomPoint(moveBounds);
            //Debug.Log("[MoveRandomPoint]: changing to new movement point - " + movementPoint);
        }
        MoveToPointM(movementPoint);
	}

    public Vector3 RandomPoint(GameObject moveBounds) {
        // find max + min of boundary on movement
        Vector3 minPt = moveBounds.transform.collider.bounds.min;
        Vector3 maxPt = moveBounds.transform.collider.bounds.max;

        // select random point w/in bounds
        Vector3 movePt = minPt;
        Debug.Log("point distance X: " + (maxPt.x - minPt.x));
        Debug.Log("point distance Y: " + (maxPt.y - minPt.y));

        movePt.x += Random.Range(0.0f, 1.0f) * (maxPt.x - minPt.x);
        movePt.y += Random.Range(0.0f, 1.0f) * (maxPt.y - minPt.y);
        movePt.z = transform.position.z;

        return movePt;
    }
}
