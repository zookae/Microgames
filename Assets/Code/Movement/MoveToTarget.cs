using UnityEngine;
using System.Collections;

public class MoveToTarget : MoveControl {

    /// <summary>
    /// Target game object to move to
    /// </summary>
    public Transform movementTarget;

	// Update is called once per frame
	void Update () {
        if (movementTarget != null) {
            float distance = Vector3.Distance(transform.position, movementTarget.position);
            
            if (distance > minDist) {
                this.moveToTarget(movementTarget.position);
            }
        }
	}
}
