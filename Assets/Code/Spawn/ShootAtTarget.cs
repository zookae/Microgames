﻿using UnityEngine;
using System.Collections;

public class ShootAtTarget : Shoot {

    /// <summary>
    /// Object to fire toward
    /// </summary>
    public Transform moveTarget;

    public override GameObject Fire() {
        Debug.Log("called Fire from ShootAtTarget");
        if (moveTarget != null) {
            GameObject bullet = SpawnTriggerable();
            bullet.AddComponent<MoveToTarget>();
            bullet.GetComponent<MoveToTarget>().movementTarget = moveTarget;
            bullet.GetComponent<MoveToTarget>().moveRate = bulletSpeed;
            return bullet;
        }
        return null; // no target so no bullet for you!
    }

    /// <summary>
    /// Create a bullet and set it to move in a given direction
    /// </summary>
    public GameObject ShootAtTar() {
        Debug.Log("called ShootAtTar");
        if (moveTarget != null) {

            Debug.Log("[ShootAtTarget] bulletspeed: " + bulletSpeed);

            GameObject bullet = SpawnTriggerable();
            bullet.AddComponent<MoveToTarget>();
            bullet.GetComponent<MoveToTarget>().movementTarget = moveTarget;
            bullet.GetComponent<MoveToTarget>().moveRate = bulletSpeed;
            return bullet;
        }
        return null; // no target so no bullet for you!
    }

    /// <summary>
    /// Fire at a target but have bullet destroyed if moves outside given bounds
    /// </summary>
    /// <param name="moveBounds"></param>
    /// <returns></returns>
    public GameObject ShootAtTar(GameObject moveBounds, float lifespan) {
        GameObject newObj = ShootAtTar();
        if (newObj != null) { // test for whether there was a target
            if (moveBounds != null) {
                newObj.AddComponent<CleanupBound>();
                newObj.GetComponent<CleanupBound>().boundingObject = moveBounds;
            }
            if (lifespan > 0) {
                newObj.AddComponent<DestroyTime>();
                newObj.GetComponent<DestroyTime>().lifetimer = lifespan;
            }
        }
        
        return newObj;
    }

    /// <summary>
    /// Fire at a target but have bullet destroyed if moves outside given bounds
    /// </summary>
    /// <param name="moveBounds"></param>
    /// <returns></returns>
    public GameObject ShootAtTar(GameObject moveBounds) {
        return ShootAtTar(moveBounds, -1);
    }

    /// <summary>
    /// Fire at a target but have bullet self-destruct after a time limit
    /// </summary>
    /// <param name="moveBounds"></param>
    /// <returns></returns>
    public GameObject ShootAtTar(float lifespan) {
        return ShootAtTar(null, lifespan);
    }
	
}
