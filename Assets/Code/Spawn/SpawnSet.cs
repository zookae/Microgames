using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnSet : Spawn {

    /// <summary>
    /// Prefab for targets to generate
    /// </summary>
    public GameObject target;

    /// <summary>
    /// Whether to spawn targets that obey gravity or are kinematic
    /// </summary>
    //public bool haveGravity = false;
    /// <summary>
    /// Number of objects to spawn in the set
    /// </summary>
    public int numObjects;
    /// <summary>
    /// Maximum distance of any object from spawner center
    /// </summary>
    public float maxDistance;
    /// <summary>
    /// List of spawned objects
    /// </summary>
    private List<GameObject> spawnList;
	
	/// <summary>
	/// Target object to move toward
	/// </summary>
	public Transform movementTarget; // TODO : replace with generic script to attach
	/// <summary>
	/// Speed for spawned objects to move toward target
	/// </summary>
	public float movementRate;
	
    // Use this for initialization
	void Start () {
        spawnList = new List<GameObject>();
		
		float xmin = this.transform.position.x - maxDistance;
		float xmax = this.transform.position.x + maxDistance;
		float zmin = this.transform.position.z - maxDistance;
		float zmax = this.transform.position.z + maxDistance;
		
        // 0. compute radius of sphere centered on object to use for overlap detection
        float targetSize = Vector3.Distance(target.transform.collider.bounds.min, target.transform.collider.bounds.max);
        
        // repeatedly try to generate
        while (spawnList.Count < numObjects) {
            // 1. pick point
            Vector3 position = new Vector3(Random.Range(xmin, xmax), 1, Random.Range(zmin, zmax)); // x,z plane
						
            // 2. ensure not more than maxDistance away
            if (Vector3.Distance(position, this.transform.position) > maxDistance) {
                continue;
            }

            // 3. check if occupied
            if (!Physics.CheckSphere(position, targetSize)) {
                // 4. spawn object there if not
                GameObject newTarget = SpawnTriggerable(); // generate object
                newTarget.transform.position = position; // set position
				
				newTarget.gameObject.AddComponent<MoveToTarget>();
				newTarget.GetComponent<MoveToTarget>().movementTarget = movementTarget;
				newTarget.GetComponent<MoveToTarget>().moveRate = movementRate * Random.Range(0.75f, 1.25f); // give some randomness to speed

                newTarget.gameObject.AddComponent<DestroyPrint>();
				
                spawnList.Add(newTarget); // add to list of generated
            }
        }
	}
}
