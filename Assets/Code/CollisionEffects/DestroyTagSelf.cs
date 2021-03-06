﻿using UnityEngine;
using System.Collections;

public class DestroyTagSelf : MonoBehaviour {

    public string destroyTag;

    /// <summary>
    /// Destroy objects with a given tag
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerEnter(Collider col) {
        Debug.Log("destroy : entered trigger");
        if (col.CompareTag(destroyTag)) {
            Destroy(this.gameObject);
        }
        
    }
}
