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

    /// <summary>
    /// Select a random point,
    /// constrained by bounds object defining space of movement
    /// </summary>
    /// <param name="moveBounds">Boundary for movement</param>
    /// <returns>New point</returns>
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

    /// <summary>
    /// Select a random point along the X axis,
    /// constrained by bounds object defining space of X movement
    /// </summary>
    /// <param name="moveBounds">Boundary for movement</param>
    /// <returns>New point</returns>
    public Vector3 RandomPointX(GameObject moveBounds) {
        // find max + min of boundary on movement
        Vector3 minPt = moveBounds.transform.collider.bounds.min;
        Vector3 maxPt = moveBounds.transform.collider.bounds.max;

        float minX = transform.position.x - minPt.x;
        float maxX = maxPt.x - transform.position.x ;

        Vector3 movePt = transform.position;

        // move random distance along X toward further bound
        if (maxX > minX) {
            movePt.x += Random.Range(0.0f, 1.0f) * maxX;
        } else {
            movePt.x -= Random.Range(0.0f, 1.0f) * minX;
        }

        //movePt.x = minPt.x + Random.Range(0.0f, 1.0f) * maxPt.x;
        movePt.z = transform.position.z;

        return movePt;
    }

    /// <summary>
    /// Select a random point along the Y axis,
    /// constrained by bounds object defining space of Y movement
    /// </summary>
    /// <param name="moveBounds">Boundary for movement</param>
    /// <returns>New point</returns>
    public Vector3 RandomPointY(GameObject moveBounds) {
        // find max + min of boundary on movement
        Vector3 minPt = moveBounds.transform.collider.bounds.min;
        Vector3 maxPt = moveBounds.transform.collider.bounds.max;

        float minY = transform.position.y - minPt.y;
        float maxY = maxPt.y - transform.position.y;

        Vector3 movePt = transform.position;

        // move random distance along Y toward further bound
        if (maxY > minY) {
            movePt.y += Random.Range(0.0f, 1.0f) * maxY;
        } else {
            movePt.y -= Random.Range(0.0f, 1.0f) * minY;
        }

        movePt.z = transform.position.z;

        return movePt;
    }


}
