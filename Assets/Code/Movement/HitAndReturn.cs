using UnityEngine;
using System.Collections;

public class HitAndReturn : MoveControl {

    private bool isMoving = false;

    private Vector3 startPos;
    public Vector3 endPos; // TODO: alternative representation is movement as delta from position

    void Awake() {
        startPos = transform.position;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (isRunning) {
            // done moving if at start position
            if (transform.position == startPos) {
                isMoving = false;
            }

            // move when click is registered; only if not already moving
            if (Input.GetMouseButtonDown(0) && !isMoving) {
                isMoving = true;
                moveTarget = endPos;
            }

            if (isMoving) {
                transform.position = Vector3.MoveTowards(transform.position, moveTarget, moveRate * Time.deltaTime); // direct bouncing

                // move to end position if more than minimum distance away
                if (Vector3.Distance(transform.position, endPos) < minDist) {
                    moveTarget = startPos;
                }
            }
        }
	}
}
