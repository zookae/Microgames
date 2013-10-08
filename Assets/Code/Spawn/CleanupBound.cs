using UnityEngine;
using System.Collections;

/// <summary>
/// Destroy object if it moves outside of given bounds
/// </summary>
public class CleanupBound : MonoBehaviour {

    /// <summary>
    /// Object defining bounds of movement space
    /// </summary>
    public GameObject boundingObject;

	
	// Update is called once per frame
	void Update () {

        // test if outside boundary -> destroy if so
        if (!IsInsideBoundary(transform, boundingObject)) {
            //Debug.Log(transform.name + " is outside the bounding " + boundingObject.name);
            Destroy(gameObject);
        }
	}

    void OnDestroy() {

    }

    /// <summary>
    /// Test whether a transform is within the bounds of another object along X-Y projection
    /// </summary>
    /// <param name="transform">Transform to test for being in bounds</param>
    /// <param name="boundingObject">Bounds to use</param>
    /// <returns></returns>
    public static bool IsInsideBoundary(Transform transform, GameObject boundingObject) {
        Bounds boundary = boundingObject.transform.collider.bounds; // cache to save some computation
        Vector3 transsize = transform.collider.bounds.size; // cache to save some computation

        // test for exceeding bounds on left
        if (transform.position.x + transsize.x < boundary.min.x) {
            return false;
        }
            // test for exceeding bounds on right
        else if (transform.position.x - transsize.x > boundary.max.x) {
            return false;
        }
            // test for exceeding bounds on top
        else if (transform.position.y - transsize.y > boundary.max.y) {
            return false;
        }
            // test for exceeding bounds on bottom
        else if (transform.position.y + transsize.y < boundary.min.y) {
            return false;
        }

        return true;
    }
}
