using UnityEngine;
using System.Collections;

public class BasicAvoidX : MonoBehaviour {
    /// <summary>
    /// Whether movement control uses forces or direction translation
    /// </summary>
    public bool moveByForce = false;

    /// <summary>
    /// Tag of objects for NPC to avoid
    /// </summary>
    public string avoidTag;

    /// <summary>
    /// Frequency to poll environment for new objects
    /// </summary>
    public float senseFrequency;

    /// <summary>
    /// Frequency to consider actions
    /// </summary>
    public float actFrequency;

    /// <summary>
    /// Minimum distance to consider taking avoid action
    /// </summary>
    public float minActDistance;

    /// <summary>
    /// Speed for agent to use for avoiding
    /// </summary>
    public float moveSpeed;

    public float moveForce;

    private float senseDelta;
    private float actDelta;

    public GameObject[] avoidObject;

    public float avoidMagnitude = 0.0f;

    public Vector3 moveVec;

    public int avoidPos;
    public int avoidNeg;

    private float initialZ;

    void Start() {
        moveVec = transform.position;
        if (moveByForce && gameObject.GetComponent<MoveByKeyForce>() != null) {
            moveForce = gameObject.GetComponent<MoveByKeyForce>().force;
        }
        initialZ = transform.position.z;
    }
	
	// Update is called once per frame
	void Update () {
        // sensing
        senseDelta += Time.deltaTime; // track time since last sensing
        if (senseDelta > senseFrequency) {
            avoidObject = GameObject.FindGameObjectsWithTag(avoidTag);
            senseDelta = 0;
        }

        // track number of objects on either side to avoid
        avoidPos = 0;
        avoidNeg = 0;
        avoidMagnitude = 0.0f;

        if (avoidObject != null && avoidObject.Length > 0) {
            // deciding
            foreach (GameObject go in avoidObject) {
                if (go == null)
                    continue;
                // TODO :  predict position + avoid that; problem is non-physics-based movement

                // compute min distance to collider bounds
                float goDist1 = transform.collider.bounds.max.x - go.transform.position.x;
                float goDist2 = transform.collider.bounds.min.x - go.transform.position.x;
                float goDist = Mathf.Min(goDist1, goDist2);

                //Debug.Log("[BasicAvoidX] distance to object " + go.name + ": (" + goDist1 + "," + goDist2 + " -> " + goDist + ")");

                if (Mathf.Abs(goDist) < minActDistance) {
                    if (goDist < 0)
                        avoidNeg++;
                    if (goDist > 0)
                        avoidPos++;

                    // move in opposite direction of object
                    avoidMagnitude += Mathf.Abs(goDist);
                }
            }

            // (1) pick direction to move to avoid
            // (2) move
            // (3) reset direction picked
        }

        // acting
        if (avoidMagnitude > 0.0f) {
            // average over applied avoiding movements
            if (avoidNeg > avoidPos)
                avoidMagnitude *= -1; // flip direction if more on other side

            
            if (moveByForce) {
                moveVec.x = avoidMagnitude;
                moveVec.y = 0.0f;
                this.rigidbody.AddForce(moveVec * moveForce, ForceMode.Force);
                this.rigidbody.rotation = new Quaternion();
            }
            else {
                moveVec.x += avoidMagnitude;
                transform.position = Vector3.MoveTowards(transform.position,
                    moveVec,
                    moveSpeed * Time.deltaTime);
            }
            
        }

        if (transform.position.z != initialZ) {
            Vector3 fixPos = transform.position;
            fixPos.z = initialZ;
            transform.position = fixPos;
        }

	}
}