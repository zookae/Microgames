using UnityEngine;
using System.Collections;

public abstract class Shoot : Spawn {

    /// <summary>
    /// Speed for shot object to move
    /// </summary>
    public float moveSpeed;

    public abstract GameObject Fire();

    public GameObject Fire(GameObject moveBounds, float lifespan) {
        GameObject newObj = Fire();
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
}
