using UnityEngine;
using System.Collections;

public class MoveTowardTarget : MoveControl {

    /// <summary>
    /// Target game object to move to
    /// </summary>
    public Transform targetTransform;
    private Vector3 initialTarget;

    public bool stopAtTarget = false;
    private Vector3 moveVector;

    void Start() {
        initialTarget = targetTransform.position;
        moveVector = initialTarget - transform.position;
    }

	// Update is called once per frame
	void Update () {
        if (initialTarget != null) {
            if (stopAtTarget) {
                float distance = Vector3.Distance(transform.position, initialTarget);
                if (distance > minDist) {
                    this.moveToTarget(initialTarget);
                }
            }
            else {
                transform.position = Vector3.MoveTowards(transform.position, 
                    transform.position + moveVector, moveRate * Time.deltaTime);
            }
            
        }
	}
}
