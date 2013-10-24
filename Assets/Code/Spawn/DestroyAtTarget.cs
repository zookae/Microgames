using UnityEngine;
using System.Collections;

public class DestroyAtTarget : DestroyBehavior {

    /// <summary>
    /// Target point to self-destruct if reached
    /// </summary>
    public Transform target;

    private Vector3 targetPoint;

    public float minDistance = 0.1f;

    void Start() {
        targetPoint = target.position;
    }

	// Update is called once per frame
	void Update () {
        DestroyOnPoint(targetPoint, this.minDistance);
	}
}
