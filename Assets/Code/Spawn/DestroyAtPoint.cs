using UnityEngine;
using System.Collections;

public class DestroyAtPoint : DestroyBehavior {

    /// <summary>
    /// Target point to self-destruct if reached
    /// </summary>
    public Vector3 target;

    public float minDistance = 0.1f;

	// Update is called once per frame
	void Update () {
        DestroyOnPoint(target, this.minDistance);
	}


}
