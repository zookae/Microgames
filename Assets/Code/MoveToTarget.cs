using UnityEngine;
using System.Collections;

public class MoveToTarget : MoveControl {

    public GameObject movementTarget;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float distance = Vector3.Distance(transform.position, movementTarget.transform.position);
        //Debug.Log("distance to target : " + distance);
        if (distance > minDist) {
            transform.position = Vector3.MoveTowards(transform.position, movementTarget.transform.position, moveRate * Time.deltaTime); // direct bouncing
        }
	}

    /// <summary>
    /// Move this object to the given target at a fixed rate
    /// </summary>
    /// <param name="target"></param>
    //public void moveToTarget(Vector3 moveGoal) {
    //    float distance = Vector3.Distance(transform.position, moveGoal);
    //    //Debug.Log("distance to target : " + distance);
    //    if (distance > minDist) {
    //        transform.position = Vector3.MoveTowards(transform.position, moveGoal, moveRate * Time.deltaTime); // direct bouncing
    //    }
    //}
}
