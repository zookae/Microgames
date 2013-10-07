using UnityEngine;
using System.Collections;

/// <summary>
/// AI to have agent avoid colliding with objects according to their tag
/// </summary>
public class BasicAvoid : MonoBehaviour {

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

    private Vector3 avoidMovement;

    
	// Update is called once per frame
	void Update () {
        // sensing
        senseDelta += Time.deltaTime; // track time since last sensing
        if (senseDelta > senseFrequency) {
            avoidObject = GameObject.FindGameObjectsWithTag(avoidTag);
            senseDelta = 0;
        }

        avoidMovement = transform.position; // vector for avoiding movement
        int numAvoid = 0;

        // deciding
        foreach (GameObject go in avoidObject) {
            if (go == null)
                continue;
            float goDist = Vector3.Distance(transform.position, go.transform.position);
            if (goDist < minActDistance) {
                // move in opposite direction of object
                avoidMovement += go.transform.position;
                numAvoid++;
            }
        }

        // acting
        if (avoidMovement != transform.position) {
            // average over applied avoiding movements
            avoidMovement = avoidMovement / numAvoid; 
            avoidMovement.z = 0.0f;
            transform.position = Vector3.MoveTowards(transform.position, avoidMovement,
                moveSpeed * Time.deltaTime);
        }
        
	}
}
