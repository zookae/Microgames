using UnityEngine;
using System.Collections;

public class BasicAvoidX : MonoBehaviour {

    /// <summary>
    /// Tag of objects for NPC to avoid
    /// </summary>
    public string avoidTag;

    /// <summary>
    /// Frequency to poll environment for new objects
    /// </summary>
    public float senseFrequency;

    /// <summary>
    /// Minimum distance to consider taking avoid action
    /// </summary>
    public float minActDistance;

    /// <summary>
    /// Speed for agent to use for avoiding
    /// </summary>
    public float moveSpeed;

    private float senseDelta;

    private GameObject[] avoidObject;

    private float avoidMagnitude = 0.0f;

	
	// Update is called once per frame
	void Update () {
        // sensing
        senseDelta += Time.deltaTime; // track time since last sensing
        if (senseDelta > senseFrequency) {
            avoidObject = GameObject.FindGameObjectsWithTag(avoidTag);
            senseDelta = 0;
        }

        // track number of objects on either side to avoid
        int avoidPos = 0;
        int avoidNeg = 0;

        if (avoidObject != null && avoidObject.Length > 0) {
            // deciding
            foreach (GameObject go in avoidObject) {
                if (go == null)
                    continue;
                // TODO :  predict position + avoid that; problem is non-physics-based movement
                float goDist = transform.position.x - go.transform.position.x;
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

            // acting
            if (avoidMagnitude > 0.0f) {
                // average over applied avoiding movements
                if (avoidNeg > avoidPos)
                    avoidMagnitude *= -1; // flip direction if more on other side

                Vector3 avoidVec = new Vector3(avoidMagnitude, 0, 0);
                transform.position = Vector3.MoveTowards(transform.position, 
                    transform.position + avoidVec,
                    moveSpeed * Time.deltaTime);
            }
        }
	}
}
