using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargetGenerator : MonoBehaviour {

    /// <summary>
    /// Prefab for targets to generate
    /// </summary>
    public Transform target;
    /// <summary>
    /// Tag to assign to new targets
    /// </summary>
    public List<string> tags;
    /// <summary>
    /// Speed of newly spawned objects
    /// </summary>
    public float moveSpeed = 10.0f;
    /// <summary>
    /// Timing between spawns
    /// </summary>
    public float serialSpacing = 1.0f;    /// <summary>
    /// Whether to use randomly timed delays between spawns
    /// </summary>
    public bool isRandomlySpaced = false;

    /// <summary>
    /// Track time spent
    /// </summary>
    public float ticker = 0.0f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        ticker += Time.deltaTime;
        if (Mathf.Round(ticker / serialSpacing) == 0) {
            if (isRandomlySpaced) {
                ticker -= serialSpacing * (Random.Range(0, 100) / 100.0f);
            } else {
                ticker -= serialSpacing;
            }
            SpawnTarget(target, moveSpeed);
        }
	}

    void SpawnTarget(Transform spawnObj, float moveSpeed) {
        // create new instance of prefab
        Transform newTarget = (Transform)GameObject.Instantiate(spawnObj, transform.position, transform.rotation);

        // 
        newTarget.gameObject.AddComponent<Rigidbody>();
        newTarget.GetComponent<Rigidbody>().useGravity = false;
        newTarget.GetComponent<Rigidbody>().isKinematic = true;


        newTarget.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * moveSpeed);
        foreach (string tag in tags) {
            newTarget.tag = tag;
        }
    }
}
