using UnityEngine;
using System.Collections;

public class DestroyTarget : MonoBehaviour {

    public string destroyTag;

    void OnTriggerEnter(Collider col) {
        Debug.Log("destroy : entered trigger");
        if (col.CompareTag(destroyTag)) {
            Destroy(col.gameObject);
        }
        
    }
}
