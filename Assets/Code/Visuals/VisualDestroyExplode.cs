using UnityEngine;
using System.Collections;

public class VisualDestroyExplode : MonoBehaviour {

    /// <summary>
    /// Explosion to trigger
    /// </summary>
    public GameObject explosion;

    /// <summary>
    /// Duration for explosion to last
    /// </summary>
    public float lifespan = 3.0f;


    //http://answers.unity3d.com/questions/174581/explosion-on-collision.html
    void OnDestroy() {
        GameObject explode = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
        Destroy(gameObject); // destroy self
        Destroy(explode, lifespan);
    }
}
