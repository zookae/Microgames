using UnityEngine;
using System.Collections;

public class ClickFireObject : ShootAtTarget {

    /// <summary>
    /// Object to fire at when clicking
    /// </summary>
    public GameObject fireTarget;

    void Start() {
        moveTarget = fireTarget.transform.position;
    }

	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0)) {
            moveTarget = fireTarget.transform.position;
            ShootAtTar();
        }
	}
}
