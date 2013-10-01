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
    public Transform moveTarget;

    /// <summary>
    /// Create a bullet and set it to move in a given direction
    /// </summary>
    public GameObject ShootAtTar() {
        Debug.Log("called ShootAtTar");
        GameObject bullet = SpawnTriggerable();
        bullet.AddComponent<MoveToTarget>();
        if (moveTarget != null) {
            bullet.GetComponent<MoveToTarget>().movementTarget = moveTarget;
        } else {
            // default to silly behavior of not moving
            bullet.GetComponent<MoveToTarget>().movementTarget = transform;
        }

        bullet.GetComponent<MoveToTarget>().moveRate = moveSpeed;

        return bullet;
    }

    /// <summary>
    /// Fire at a target but have bullet destroyed if moves outside given bounds
    /// </summary>
    /// <param name="moveBounds"></param>
    /// <returns></returns>
    public GameObject ShootAtTar(GameObject moveBounds, float lifespan) {
        GameObject newObj = ShootAtTar();
        if (moveBounds != null) {
            newObj.AddComponent<CleanupBound>();
            newObj.GetComponent<CleanupBound>().boundingObject = moveBounds;
        }
        if (lifespan > 0) {
            newObj.AddComponent<DestroyTime>();
            newObj.GetComponent<DestroyTime>().lifetimer = lifespan;
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
