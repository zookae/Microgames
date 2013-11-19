using UnityEngine;
using System.Collections;

public class ShootAtPoint : Shoot {

    /// <summary>
    /// Object to fire toward
    /// </summary>
    public Vector3 moveTarget;

    /// <summary>
    /// Create a bullet and set it to move in a given direction
    /// </summary>
    public GameObject ShootAtTar() {
        //Debug.Log("called ShootAtTar");
        GameObject bullet = SpawnTriggerable();
        bullet.AddComponent<MoveToPoint>();
        bullet.GetComponent<MoveToPoint>().movementPoint = moveTarget;
        bullet.GetComponent<MoveToPoint>().moveTarget = moveTarget;
        bullet.GetComponent<MoveToPoint>().moveRate = bulletSpeed;

        return bullet;
    }

    public override GameObject Fire() {
        return ShootAtTar();
    }

}
