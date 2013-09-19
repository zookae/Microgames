using UnityEngine;
using System.Collections;

public class ClickFireTarget : ShootAtTarget {

    public Camera cam;

    Vector3 p;

	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0)) {
            moveTarget = TargetLocation(Input.mousePosition);
            ShootAtTar();
        }
	}

    /// <summary>
    /// Return the coordinates of the point where the mouse clicked
    /// </summary>
    /// <param name="mousePos"></param>
    /// <returns></returns>
    public Vector3 TargetLocation(Vector3 mousePos) {
        mousePos.z = 10; // need Z value for ScreenToWorldPoint to work
        Vector3 p = cam.ScreenToWorldPoint(mousePos);
        p.z = transform.position.z; // z axis to plane defined by player

        return p;
    }
}
