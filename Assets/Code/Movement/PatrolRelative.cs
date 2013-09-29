using UnityEngine;
using System.Collections;

public class PatrolRelative : MoveControl {

    public Vector3 patrolA;
    public Vector3 patrolB;

    private Vector3 pointA;
    private Vector3 pointB;

	// Use this for initialization
	void Start () {
        pointA = transform.position + patrolA;
        pointB = transform.position + patrolB;
        moveTarget = pointB;
	}
	
	// Update is called once per frame
	void Update () {
        // TODO : compose moveTo
        if (isRunning) {
            transform.position = Vector3.MoveTowards(transform.position, moveTarget, moveRate * Time.deltaTime); // direct bouncing

            if (Vector3.Distance(transform.position, pointA) < minDist) {
                moveTarget = pointB;
            }
            if (Vector3.Distance(transform.position, pointB) < minDist) {
                moveTarget = pointA;
            }
        }
	}
}
