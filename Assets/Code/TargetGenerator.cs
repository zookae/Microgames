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
    /// Timing between spawns
    /// </summary>
    public float serialSpacing = 1.0f;
    /// <summary>
    /// Time before starting spawns
    /// </summary>
    public float serialDelay = 0.0f;
    /// <summary>
    /// Whether to use randomly timed delays between spawns
    /// </summary>
    public bool isRandomlySpaced = false; // TODO : fix to ensure no overlaps
    /// <summary>
    /// Speed for objects to move
    /// </summary>
    public float moveSpeed = 5.0f;
    /// <summary>
    /// Direction for the objects to move
    /// </summary>
    public MoveDirection moveDirection;

    /// <summary>
    /// Track time spent
    /// </summary>
    private float ticker = 0.0f;
    /// <summary>
    /// Internal tracking of spacing for use with randomization
    /// </summary>
    private float serialSeparation;

	// Use this for initialization
	void Start () {
        ticker -= serialDelay;
        serialSeparation = serialSpacing;
	}
	
	// Update is called once per frame
	void Update () {
        if (GameState.Singleton.CurrentState == State.Running) {
            ticker += Time.deltaTime;
            if (isRandomlySpaced) {
                serialSeparation = serialSpacing * (Random.Range(50, 100) / 100.0f);
            }
            if (ticker > serialSeparation) {
                ticker -= serialSeparation;
                //SpawnTarget();
                SpawnMovingTarget();
            }
        }
	}

    void SpawnMovingTarget() {
        Transform newTarget = (Transform)GameObject.Instantiate(target, transform.position, transform.rotation);
        newTarget.gameObject.AddComponent<Rigidbody>();
        newTarget.GetComponent<Rigidbody>().useGravity = false;
        newTarget.GetComponent<Rigidbody>().isKinematic = true;
        //newTarget.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * moveSpeed);
        newTarget.gameObject.AddComponent<MoveInDirection>();
        newTarget.GetComponent<MoveInDirection>().dir = moveDirection;
        newTarget.GetComponent<MoveInDirection>().moveRate = moveSpeed;

        newTarget.GetComponent<BoxCollider>().isTrigger = true;

        // assign set of tags for other components to use
        foreach (string tag in tags) {
            newTarget.tag = tag;
        }
    }

    void SpawnTarget() {
        // create new instance of prefab
        Transform newTarget = (Transform)GameObject.Instantiate(target, transform.position, transform.rotation);

        // assign Rigidbody to place in space and apply physics
        //newTarget.gameObject.AddComponent<Rigidbody>();
        //newTarget.GetComponent<Rigidbody>().useGravity = false;
        //newTarget.GetComponent<Rigidbody>().isKinematic = true;
        
        // assign MoveInDirection to dictate movement behavior
        //newTarget.gameObject.AddComponent<MoveInDirection>();
        //newTarget.GetComponent<MoveInDirection>().moveRate = moveSpeed;
        //newTarget.GetComponent<MoveInDirection>().dir = moveDirection;

        //newTarget.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * moveSpeed);

        // assign set of tags for other components to use
        foreach (string tag in tags) {
            newTarget.tag = tag;
        }
    }
}
