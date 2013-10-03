using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnWave : Spawn {

    //public PatrolRelativeX moveBehavior;
    public float xSpeed;
    public float xMin;
    public float xMax;

    public float ySpeed;

    //public ShootAtTarget shootBehavior;

    public MovementType MovementOnX;
    public MovementType MovementOnY;

    public float spawnFrequency;

    private float runningTime;

    public GameObject bullet;

    private GameObject background;
    private GameObject player;

    void Awake() {
        background = GameObject.Find("Background");
        player = GameObject.Find("Player");
    }

	// Update is called once per frame
	void Update () {
        runningTime += Time.deltaTime;

        if (runningTime > spawnFrequency) {
            runningTime = 0.0f; // reset time since entity spawned

            spawnEnemy();
        }
	}

    public enum MovementType {
        PatrolRelativeX,
        MoveToRandomPointX,
        MoveInDirectionDown
    }

    GameObject spawnEnemy() {
        // create the object
        GameObject newTarget = (GameObject)GameObject.Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube)
            , transform.position, transform.rotation);

        newTarget.transform.localScale *= 0.5f; // rescale
        newTarget.renderer.material.color = Color.red; // apply color

        // add physics behavior
        newTarget.gameObject.AddComponent<Rigidbody>();
        newTarget.GetComponent<Rigidbody>().useGravity = false;
        newTarget.GetComponent<Rigidbody>().isKinematic = true;

        // set trigger property so it can be noticed by OnTriggerEnter
        newTarget.GetComponent<BoxCollider>().isTrigger = true;

        // assign set of tags for other components to use
        newTarget.tag = assignTag;

        //// add movement along X axis behavior
        //newTarget.gameObject.AddComponent<PatrolRelativeX>();
        //newTarget.gameObject.GetComponent<PatrolRelativeX>().moveRate = xSpeed;
        //newTarget.gameObject.GetComponent<PatrolRelativeX>().patrolMin.x = xMin;
        //newTarget.gameObject.GetComponent<PatrolRelativeX>().patrolMax.x = xMax;

        switch (MovementOnX) {
            case MovementType.PatrolRelativeX:
                // attach / parse from XML
                newTarget.gameObject.AddComponent<PatrolRelativeX>();
                newTarget.gameObject.GetComponent<PatrolRelativeX>().moveRate = xSpeed;
                newTarget.gameObject.GetComponent<PatrolRelativeX>().patrolMin.x = xMin;
                newTarget.gameObject.GetComponent<PatrolRelativeX>().patrolMax.x = xMax;
                break;
            case MovementType.MoveToRandomPointX:
                // attach / parse from XML
                newTarget.gameObject.AddComponent<MoveToRandomPointX>();
                newTarget.gameObject.GetComponent<MoveToRandomPointX>().moveRate = xSpeed;
                newTarget.gameObject.GetComponent<MoveToRandomPointX>().moveBounds = background;
                break;
            default:
                break;
        }

        switch (MovementOnY) {
            case MovementType.MoveInDirectionDown:
                // add movement along Y axis behavior
                newTarget.gameObject.AddComponent<MoveInDirection>();
                newTarget.gameObject.GetComponent<MoveInDirection>().moveRate = ySpeed;
                newTarget.gameObject.GetComponent<MoveInDirection>().dir = MoveDirection.Down;
                break;

            default:
                break;
        }

        // add shooting behavior
        newTarget.gameObject.AddComponent<NPCShootAtTarget>();
        newTarget.gameObject.GetComponent<NPCShootAtTarget>().spawn = bullet;
        newTarget.gameObject.GetComponent<NPCShootAtTarget>().bulletBounds = background;
        newTarget.gameObject.GetComponent<NPCShootAtTarget>().bulletLife = 10.0f;
        newTarget.gameObject.GetComponent<NPCShootAtTarget>().moveSpeed = 5.0f;
        newTarget.gameObject.GetComponent<NPCShootAtTarget>().frequency = 1.5f;
        if (player != null) {
            newTarget.gameObject.GetComponent<NPCShootAtTarget>().moveTarget = player.transform;
        }

        return newTarget;

    }
}
