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
    public List<GameObject> spawnList;
	
	/// <summary>
	/// Target object to move toward
	/// </summary>
	public Transform movementTarget; // TODO : replace with generic script to attach
	/// <summary>
	/// Speed for spawned objects to move toward target
	/// </summary>
	public float movementRate;
	
    // Use this for initialization
	void Awake () {

        Debug.Log("[SpawnSet] started spawning");

        spawnList = new List<GameObject>();
		
		float xmin = this.transform.position.x - maxDistance;
		float xmax = this.transform.position.x + maxDistance;
		float ymin = this.transform.position.y - maxDistance;
		float ymax = this.transform.position.y + maxDistance;
		
        // 0. compute radius of sphere centered on object to use for overlap detection
        float targetSize = Vector3.Distance(target.transform.collider.bounds.min, target.transform.collider.bounds.max);

        Debug.Log("[SpawnSet] target size is: " + targetSize);

        // repeatedly try to generate
        while (spawnList.Count < numObjects) {
            // 1. pick point
            Vector3 position = new Vector3(Random.Range(xmin, xmax), Random.Range(ymin, ymax), 0); // x,y plane
            
            // 2. ensure not more than maxDistance away
            if (Vector3.Distance(position, this.transform.position) > maxDistance) {
                continue;
            }

            Debug.Log("[SpawnSet] target position is: " + position);

            // 3. check if occupied
            if (!Physics.CheckSphere(position, targetSize)) {
                // 4. spawn object there if not
                GameObject newTarget = SpawnTriggerable(); // generate object
                newTarget.transform.position = position; // set position

                Debug.Log("[SpawnSet] new target name is: " + newTarget.name);
				
				newTarget.gameObject.AddComponent<MoveToTarget>();
				newTarget.GetComponent<MoveToTarget>().movementTarget = movementTarget;
				newTarget.GetComponent<MoveToTarget>().moveRate = movementRate * Random.Range(0.75f, 1.25f); // give some randomness to speed

                newTarget.gameObject.AddComponent<DestroyPrint>();
				
                spawnList.Add(newTarget); // add to list of generated

                Debug.Log("[SpawnSet] spawn list length is: " + spawnList.Count);
            }
        }
	}
}
