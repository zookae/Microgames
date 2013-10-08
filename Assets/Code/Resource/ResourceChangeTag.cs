using UnityEngine;
using System.Collections;

public class ResourceChangeTag : MonoBehaviour {

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

    /// <summary>
    /// Destroy objects with a given tag
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerEnter(Collider col) {
        //Debug.Log("resource loss : entered trigger");

        if (col.CompareTag(targetTag)) {
            // get all resources
            Resource[] resources = col.gameObject.GetComponents<Resource>();

            // find those of given type
            foreach (Resource res in resources) {
                // change if possible
                if (res.resourcetype == rtype) {
                    res.ChangeValue(res.value + rchange);
                }
            }
        }
    }
}
