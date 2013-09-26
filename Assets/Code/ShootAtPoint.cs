using UnityEngine;
using System.Collections;

public class ShootAtPoint : Spawn {

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
        bullet.AddComponent<MoveToPoint>();
        bullet.GetComponent<MoveToPoint>().movementPoint = moveTarget;
        bullet.GetComponent<MoveToPoint>().moveTarget = moveTarget;
        bullet.GetComponent<MoveToPoint>().moveRate = moveSpeed;

        return bullet;
    }

}
