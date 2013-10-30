using UnityEngine;
using System.Collections;

public class MoveToClick : MoveControl {

    /// <summary>
    /// Screen position of mouse click
    /// </summary>
    public Vector3 mouseTarget;
	public Camera cam;

    public Vector3 p;

    void Start() {
        isRunning = false; // start w/o input
    }
    

	// Update is called once per frame
	void Update () {
        // TODO: should have generic way to say whether method continues when game ends
        if (GameState.Singleton.CurrentState != State.Running)
            return;

        // detect click and set goal of going there
        if (Input.GetMouseButtonDown(0)) {
            mouseTarget = Input.mousePosition;
            isRunning = true;
        }

        // move to target when running
        if (isRunning) {
            moveToMouse(mouseTarget);
            //moveToMousePhysics(mouseTarget);
        }

        // stop running when close enough to goal
        if (Vector3.Distance(moveTarget, transform.position) < minDist) {
            isRunning = false;
        }
	}

    // TODO : make version that moves to absolute world position, rather than screen-based
    /// <summary>
    /// Move in a straight line at constant rate toward a given mouse position
    /// NOTE - Moves to relative position, so will continuously move if camera follows
    /// </summary>
    /// <param name="mousePos">Position of where the mouse clicked</param>
    //public void moveToMouse(Vector3 mousePos) {
    //    Plane playerPlane = new Plane(Vector3.up, transform.position);
    //    //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    Ray ray = Camera.main.ScreenPointToRay(mousePos);

    //    float hitdistance = 0.0f;

    //    if (playerPlane.Raycast(ray, out hitdistance)) {
    //        Vector3 targetPoint = ray.GetPoint(hitdistance);
    //        moveTarget = ray.GetPoint(hitdistance);
    //        Quaternion targetRotation = Quaternion.LookRotation(moveTarget - transform.position);
    //        transform.rotation = targetRotation;
    //    }
    //    transform.position = Vector3.MoveTowards(transform.position, moveTarget, moveRate * Time.deltaTime);
    //}

    public void moveToMouse(Vector3 mousePos) {
        //Plane playerPlane = new Plane(Vector3.up, transform.position);
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		mousePos.z = 10; // need Z value for ScreenToWorldPoint to work
        p = cam.ScreenToWorldPoint(mousePos);
        p.z = transform.position.z;
        moveTarget = p;

        Quaternion targetRotation = Quaternion.LookRotation(moveTarget - transform.position);
        transform.rotation = targetRotation;
        transform.position = Vector3.MoveTowards(transform.position, moveTarget, moveRate * Time.deltaTime);

        //Ray ray = Camera.main.ScreenPointToRay(mousePos);

        //float hitdistance = 0.0f;

        //if (playerPlane.Raycast(ray, out hitdistance)) {
        //    Vector3 targetPoint = ray.GetPoint(hitdistance);
        //    moveTarget = ray.GetPoint(hitdistance);
        //    Quaternion targetRotation = Quaternion.LookRotation(moveTarget - transform.position);
        //    transform.rotation = targetRotation;
        //}
        //transform.position = Vector3.MoveTowards(transform.position, moveTarget, moveRate * Time.deltaTime);
    }

    public void moveToMousePhysics(Vector3 mousePos) {
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        float hitdistance = 0.0f;

        if (playerPlane.Raycast(ray, out hitdistance)) {
            moveTarget = ray.GetPoint(hitdistance);
            Quaternion targetRotation = Quaternion.LookRotation(moveTarget - transform.position);
            transform.rotation = targetRotation;
        }
        transform.rigidbody.AddForce(Vector3.up * 10);
    }
}
