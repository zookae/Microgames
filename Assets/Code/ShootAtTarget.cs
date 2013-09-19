using UnityEngine;
using System.Collections;

public class ShootAtTarget : Spawn {

    /// <summary>
    /// Speed for the shot object to move
    /// </summary>
    public float moveSpeed;

    /// <summary>
    /// Object to fire toward
    /// </summary>
    public Vector3 moveTarget;

    /// <summary>
    /// Create a bullet and set it to move in a given direction
    /// </summary>
    public GameObject ShootAtTar() {
        Debug.Log("called ShootAtTar");
        GameObject bullet = SpawnTriggerable();
        bullet.AddComponent<MoveToTarget>();
        bullet.GetComponent<MoveToTarget>().movementTarget = moveTarget;
        bullet.GetComponent<MoveToTarget>().moveTarget = moveTarget;
        bullet.GetComponent<MoveToTarget>().moveRate = moveSpeed;

        return bullet;
    }
	
}
