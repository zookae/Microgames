using UnityEngine;
using System.Collections;

public class ShootInDirection : Spawn {

    /// <summary>
    /// Speed for shot object to move
    /// </summary>
    public float moveSpeed;

    /// <summary>
    /// Direction for bullet to move in
    /// </summary>
    public MoveDirection moveDir;

	/// <summary>
	/// Create a bullet and set it to move in a given direction
	/// </summary>
    public GameObject ShootInDir() {
        Debug.Log("called ShootInDir");
        GameObject bullet = SpawnTriggerable();
        bullet.AddComponent<MoveInDirection>();
        bullet.GetComponent<MoveInDirection>().dir = moveDir;
        bullet.GetComponent<MoveInDirection>().moveRate = moveSpeed;

        return bullet;
    }

    public GameObject ShootInDir(GameObject moveBounds, float lifespan) {
        GameObject newObj = ShootInDir();
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

    public GameObject ShootInDir(float lifespan) {
        return ShootInDir(null, lifespan);
    }

    public GameObject ShootInDir(GameObject moveBounds) {
        return ShootInDir(moveBounds, -1);
    }
}
