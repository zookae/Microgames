﻿using UnityEngine;
using System.Collections;

public class PatrolRelativeY : MoveControl {

    public Vector3 patrolMin;
    public Vector3 patrolMax;

    private Vector3 pointMin;
    private Vector3 pointMax;
    
	// Use this for initialization
	void Start () {
        pointMin = transform.position + patrolMin;
        pointMax = transform.position + patrolMax;

        moveTarget = pointMax;
	}
	
	// Update is called once per frame
	void Update () {
        if (GameState.Singleton.CurrentState != State.Running)
            return;

        // TODO : compose moveTo
        if (isRunning) {
            // ignore movement target X and Z axes
            moveTarget.x = transform.position.x;
            moveTarget.z = transform.position.z;

            transform.position = Vector3.MoveTowards(transform.position, moveTarget, moveRate * Time.deltaTime); // direct bouncing

            if (Mathf.Abs(transform.position.y - pointMin.y) < minDist) {
                moveTarget = pointMax;
            }
            if (Mathf.Abs(transform.position.y - pointMax.y) < minDist) {
                moveTarget = pointMin;
            }
        }
	}
}
