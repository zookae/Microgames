using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResourceChangeTagOnce : MonoBehaviour {

    /// <summary>
    /// Tag of entity that colliding with triggers change
    /// </summary>
    public string targetTag;

    /// <summary>
    /// Type of the resource to change
    /// </summary>
    public ResourceType rtype;

    /// <summary>
    /// Amount of resource to change
    /// </summary>
    public float rchange;

    private List<Resource> resources;

    /// <summary>
    /// Origin point to return to
    /// </summary>
    private Vector3 origin;
    public bool hasChanged = false;

    void Start() {
        origin = transform.position;

        resources = new List<Resource>();

        // get all resources
        Resource[] possResources = gameObject.GetComponents<Resource>();
        foreach (Resource res in possResources) {
            if (res.resourcetype == rtype) {
                resources.Add(res);
            }
        }
    }

    /// <summary>
    /// Destroy objects with a given tag
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerEnter(Collider col) {
        //Debug.Log("resource loss : entered trigger");

        if (col.CompareTag(targetTag) && !hasChanged) {

            // find those of given type
            foreach (Resource res in resources) {
                // change if possible
                res.ChangeValue(res.value + rchange);
            }

            hasChanged = true;
        }
    }


    void Update() {
        if (Mathf.Abs(Vector3.Distance(transform.position, origin)) < 0.1) {
            hasChanged = false;
        }
    }
}
