using UnityEngine;
using System.Collections;

public class DestroyName : MonoBehaviour {

    public string destroyName;

    /// <summary>
    /// Only destroy the other object
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerEnter(Collider col) {
        Debug.Log("destroy : entered trigger");
        if (col.name == destroyName) {
            Destroy(col.gameObject);
            Destroy(this.gameObject);
        }

    }
}
