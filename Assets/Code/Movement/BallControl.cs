using UnityEngine;
using System.Collections;

public class BallControl : MoveControl {

    public bool hasTarget = false;
    
    //public Vector3 target;
    public Vector3 mouseTarget;

	// Use this for initialization
	void Start () {
        //target = transform.position;
        moveRate = 10.0f;
	}
	
	// Update is called once per frame
	void Update () {
        //moveToMouse(mouseTarget);
        if (isRunning && hasTarget) {
            moveToTarget(moveTarget);
        }
        //Debug.Log("target : " + target);
        
	}


}
