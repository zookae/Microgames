using UnityEngine;
using System.Collections;

public class DestroyNameBoth : MonoBehaviour {

    public string destroyName;

    /// <summary>
    /// Destroy the other object and oneself
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerEnter(Collider col) {
        //Debug.Log("destroy : entered trigger");
        if (col.name == destroyName) {
            Destroy(col.gameObject);
            Destroy(this.gameObject);
        }

    }
}
