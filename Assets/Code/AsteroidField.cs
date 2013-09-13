using UnityEngine;
using System.Collections;

public class AsteroidField : MonoBehaviour {

    public Camera cam;
    float camDepth;
    public Rigidbody playerRBody;
    public GameObject target;
    float minVelocity = 0.5f;
    float spawnInterval = 0.2f;

	// Use this for initialization
	void Start () {
        minVelocity = minVelocity * minVelocity;
        camDepth = Mathf.Abs(cam.transform.position.z);
        StartCoroutine(SpawnTarget());
	}
	
	IEnumerator SpawnTarget() {
        float x, y, z;
        while (true) {
            Vector3 vel = playerRBody.velocity.normalized;
            if (vel.sqrMagnitude > minVelocity) {

                if (Mathf.Abs(vel.y) > Mathf.Abs(vel.x)) {
                    x = Random.Range(0.0f, 1.0f);
                    if (vel.y > 0.0f) {
                        y = 1.0f;
                    } else {
                        y = 0.0f;
                    }
                } else {
                    y = Random.Range(0.0f, 1.0f);
                    if (vel.x > 0.0f) {
                        x = 1.0f;
                    } else {
                        x = 0.0f;
                    }
                }

                z = camDepth;
                Vector3 p = cam.ViewportToWorldPoint(new Vector3(x,y,z));
                Instantiate(target, p, Quaternion.identity);
            }
            yield return new WaitForSeconds(spawnInterval);
        }
	}
}
