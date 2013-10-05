using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ThresholdType {
    Above,
    Below
}

public class ResourceDestroyThresholdValue : MonoBehaviour {

    /// <summary>
    /// Type of resource threshold to check
    /// </summary>
    public ThresholdType threshType;

    /// <summary>
    /// Type of the resource to change
    /// </summary>
    public ResourceType rtype;

    /// <summary>
    /// Threshold of resource value to trigger destruction
    /// </summary>
    public float rthresh;

    private List<Resource> resourceList = new List<Resource>();

    // Use this for initialization
    void Awake() {
        Resource[] possResources = gameObject.GetComponents<Resource>();

        // only track resources of appropriate type
        // note: assumes no more resources are added to object later
        foreach (Resource pres in possResources) {
            if (pres.resourcetype == rtype) {
                resourceList.Add(pres);
            }
        }
    }

    // Update is called once per frame
    void Update() {
        foreach (Resource res in resourceList) {
            int comp = res.value.CompareTo(rthresh);
            if (comp > 0 && threshType == ThresholdType.Above) {
                Destroy(this.gameObject); // self-destruct
            }
            else if (comp < 0 && threshType == ThresholdType.Below) {
                Destroy(this.gameObject); // self-destruct
            }
        }
    }
}
