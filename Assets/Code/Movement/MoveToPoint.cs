using UnityEngine;
using System.Collections;

public class MoveToPoint : MoveControl {

    /// <summary>
    /// Point to move toward
    /// </summary>
    public Vector3 movementPoint;
	
	// Update is called once per frame
	void Update () {
        MoveToPointM(movementPoint);
	}

    /// <summary>
    /// Move toward a point
    /// </summary>
    /// <param name="movePt">Point to move toward</param>
    public void MoveToPointM(Vector3 movePt) {
        if (isRunning) {
            float distance = Vector3.Distance(transform.position, movementPoint);
            //Debug.Log("distance to target : " + distance);
            if (distance > minDist) {
                transform.position = Vector3.MoveTowards(transform.position,
                    movementPoint, moveRate * Time.deltaTime); // direct bouncing
            }
        }
    }

    /// <summary>
    /// Move toward a point only considering distance when projected to the X axis
    /// </summary>
    /// <param name="movePt">Point to move toward</param>
    public void MoveToPointXM(Vector3 movePt) {
        if (isRunning) {
            // project to distance along X axis only
            float distance = Mathf.Abs(transform.position.x - movementPoint.x);

            //Debug.Log("distance to target : " + distance);
            if (distance > minDist) {
                // create vector that uses current Y and Z positions
                Vector3 newX = transform.position;
                newX.x = movementPoint.x;

                // move toward that vector
                transform.position = Vector3.MoveTowards(transform.position,
                    newX, moveRate * Time.deltaTime); // direct bouncing
            }
        }
    }

    /// <summary>
    /// Move toward a point only considering distance when projected to the Y axis
    /// </summary>
    /// <param name="movePt">Point to move toward</param>
    public void MoveToPointYM(Vector3 movePt) {
        if (isRunning) {
            // project to distance along Y axis only
            float distance = Mathf.Abs(transform.position.y - movementPoint.y);

            //Debug.Log("distance to target : " + distance);
            if (distance > minDist) {
                // create vector that uses current X and Z positions
                Vector3 newY = transform.position;
                newY.y = movementPoint.y;

                // move toward that vector
                transform.position = Vector3.MoveTowards(transform.position,
                    newY, moveRate * Time.deltaTime); // direct bouncing
            }
        }
    }
}
