using UnityEngine;
using System.Collections;

public class DestroyBehavior : MonoBehaviour {

    public void DestroyOnPoint(Vector3 deathPoint, float minDistance) {
        if (Vector3.Distance(transform.position, deathPoint) < minDistance) {
            Destroy(this.gameObject);
        }
    }
}
