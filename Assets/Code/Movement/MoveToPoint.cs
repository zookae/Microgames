using UnityEngine;
using System.Collections;

public class MoveToPoint : MoveControl {

    public Vector3 movementPoint;
	
	// Update is called once per frame
	void Update () {
        //if (isRunning) {
        //    float distance = Vector3.Distance(transform.position, movementPoint);
        //    //Debug.Log("distance to target : " + distance);
        //    if (distance > minDist) {
        //        transform.position = Vector3.MoveTowards(transform.position,
        //            movementPoint, moveRate * Time.deltaTime); // direct bouncing
        //    }
        //}
        MoveToPointM(movementPoint);
	}

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
}
