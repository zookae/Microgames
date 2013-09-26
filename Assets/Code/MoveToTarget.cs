using UnityEngine;
using System.Collections;

public class MoveToTarget : MoveControl {

    /// <summary>
    /// Target game object to move to
    /// </summary>
    public Transform movementTarget;

	// Update is called once per frame
	void Update () {
        float distance = Vector3.Distance(transform.position, movementTarget.position);
        //Debug.Log("distance to target : " + distance);
        if (distance > minDist) {
            transform.position = Vector3.MoveTowards(transform.position, 
                movementTarget.position, moveRate * Time.deltaTime); // direct bouncing
        }
	}
}
