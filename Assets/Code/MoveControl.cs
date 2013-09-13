using UnityEngine;
using System.Collections;

public enum MoveDirection {
    Up,
    Down,
    Left,
    Right
}

public abstract class MoveControl : MonoBehaviour {

    /// <summary>
    /// Rate at which this object moves toward it's target
    /// </summary>
    public float moveRate = 1.0f;

    /// <summary>
    /// Minimum distance that is close enough to count as arriving
    /// </summary>
    public float minDist = 0.01f;

    /// <summary>
    /// Target of current movement
    /// </summary>
    public Vector3 moveTarget;

    /// <summary>
    /// Boolean flag for whether the motion should be active
    /// </summary>
    public bool isRunning = true;

    
    //// Use this for initialization
    //void Start () {
	
    //}
	
    //// Update is called once per frame
    //void Update () {
	
    //}

    /// <summary>
    /// Patrol in straight line between two points
    /// </summary>
    /// <param name="pointA"></param>
    /// <param name="pointB"></param>
    public void Patrol(Vector3 pointA, Vector3 pointB) {
        //transform.position = Vector3.Lerp(transform.position, target, moveRate * Time.deltaTime); // linear interpolation means slow near edges
        transform.position = Vector3.MoveTowards(transform.position, moveTarget, moveRate * Time.deltaTime); // direct bouncing

        if (Vector3.Distance(transform.position, pointA) < minDist) {
            moveTarget = pointB;
        }
        if (Vector3.Distance(transform.position, pointB) < minDist) {
            moveTarget = pointA;
        }
    }


    /// <summary>
    /// Move this object to the given target at a fixed rate
    /// </summary>
    /// <param name="target"></param>
    public void moveToTarget(Vector3 moveGoal) {
        float distance = Vector3.Distance(transform.position, moveGoal);
        //Debug.Log("distance to target : " + distance);
        if (distance > minDist) {
            transform.position = Vector3.MoveTowards(transform.position, moveGoal, moveRate * Time.deltaTime); // direct bouncing
        }
    }
}
