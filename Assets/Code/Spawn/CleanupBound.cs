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

    private Vector3 transsize;
    private Bounds boundary;
    private Vector3 otherSide;
    
	// Use this for initialization
	void Start () {
        boundary = boundingObject.transform.collider.bounds; // cache to save some computation
        transsize = transform.collider.bounds.size; // cache to save some computation
	}
	
	// Update is called once per frame
	void Update () {
        // test for exceeding bounds on left
        if (transform.position.x + transsize.x < boundary.min.x) {
            Debug.Log(transform.name + " is left of X bounds of " + boundingObject.name);
            Destroy(gameObject);
        }
            // test for exceeding bounds on right
        else if (transform.position.x - transsize.x > boundary.max.x) {
            Debug.Log(transform.name + " is right of X bounds of " + boundingObject.name);
            Destroy(gameObject);
        }
            // test for exceeding bounds on top
        else if (transform.position.y - transsize.y > boundary.max.y) {
            Debug.Log(transform.name + " is above Y bounds of " + boundingObject.name);
            Destroy(gameObject);
        }
            // test for exceeding bounds on bottom
        else if (transform.position.y + transsize.y < boundary.min.y) {
            Debug.Log(transform.name + " is below Y bounds of " + boundingObject.name);
            Destroy(gameObject);
        } 
	}
}
