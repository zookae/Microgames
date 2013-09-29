using UnityEngine;
using System.Collections;

public class DestroyName : MonoBehaviour {

    public string destroyName;

    void OnTriggerEnter(Collider col) {
        Debug.Log("destroy : entered trigger");
        if (col.name == destroyName) {
            Destroy(col.gameObject);
        }

    }
}
