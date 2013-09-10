using UnityEngine;
using System.Collections;

public class Patrol : MoveControl {

    public Vector3 patrolA;
    public Vector3 patrolB;

	// Use this for initialization
	void Start () {
        moveTarget = patrolB;
	}
	
	// Update is called once per frame
	void Update () {
        // TODO : compose moveTo
        if (isRunning) {
            transform.position = Vector3.MoveTowards(transform.position, moveTarget, moveRate * Time.deltaTime); // direct bouncing

            if (Vector3.Distance(transform.position, patrolA) < minDist) {
                moveTarget = patrolB;
            }
            if (Vector3.Distance(transform.position, patrolB) < minDist) {
                moveTarget = patrolA;
            }
        }
	}

    /// <summary>
    /// Patrol in straight line between two points
    /// </summary>
    /// <param name="pointA"></param>
    /// <param name="pointB"></param>
    //public void Patrol(Vector3 pointA, Vector3 pointB) {
    //    //transform.position = Vector3.Lerp(transform.position, target, moveRate * Time.deltaTime); // linear interpolation means slow near edges
    //    //transform.position = Vector3.MoveTowards(transform.position, moveTarget, moveRate * Time.deltaTime); // direct bouncing

    //    //if (Vector3.Distance(transform.position, pointA) < minDist) {
    //    //    moveTarget = pointB;
    //    //}
    //    //if (Vector3.Distance(transform.position, pointB) < minDist) {
    //    //    moveTarget = pointA;
    //    //}
    //}
}
