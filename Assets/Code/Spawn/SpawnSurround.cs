using UnityEngine;
using System.Collections;

public class SpawnSurround : MonoBehaviour {

    public Camera cam;
    float camDepth;
    public GameObject playerRbody;
    public GameObject target;
    public float minVelocity = 0.01f;
    float spawnInterval = 0.2f;
    public Vector3 vel;
    public Vector3 prevPosition;
    public Vector3 p;
    public Vector3 targetVec;

    public Vector3 playerPos;

	// Use this for initialization
	void Start () {
        minVelocity = minVelocity * minVelocity; // square min velocity to save computation when comparing against sqrt of magnitude
        camDepth = Mathf.Abs(cam.transform.position.y); // screen depth of camera
        StartCoroutine(SpawnTarget());
	}

    void FixedUpdate() {
        prevPosition = playerRbody.transform.position;
    }
	
	IEnumerator SpawnTarget() {
        float x, y, z;
        while (true) {
            //Vector3 vel = playerRBody.velocity.normalized;
			vel = (playerRbody.transform.position - prevPosition).normalized;
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
