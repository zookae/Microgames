using UnityEngine;
using System.Collections;

public abstract class Shoot : Spawn {

    /// <summary>
    /// Speed for shot object to move
    /// </summary>
    public float bulletSpeed;

    /// <summary>
    /// Seconds cooldown between shots
    /// </summary>
    public float frequency;

    /// <summary>
    /// [optional] Boundary outside of which bullet will be destroyed
    /// </summary>
    public GameObject bulletBounds;

    /// <summary>
    /// [optional] Time after which bullet will be destroyed
    /// </summary>
    public float bulletLife;

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
