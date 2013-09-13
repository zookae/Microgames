using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    Transform tr;
    public int damp = 5;
    Vector3 moveGoal;

	// Use this for initialization
	void Awake () {
        tr = transform; // cache reference for efficiency
	}
	
	// Update is called once per frame
	void Update () {
        moveGoal = Vector3.MoveTowards(tr.position, target.position, Time.deltaTime * damp); // direct bouncing
        moveGoal.z = tr.position.z;
        tr.position = moveGoal;
	}
}
