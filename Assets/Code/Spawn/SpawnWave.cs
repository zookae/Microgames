using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Possible spawned entity movement types to compose
/// </summary>
public enum MovementType {
    PatrolRelativeX,
    MoveToRandomPointX,
    MoveToRandomPointY,
    MoveInDirectionUp,
    MoveInDirectionDown,
    MoveInDirectionLeft,
    MoveInDirectionRight
}

/// <summary>
/// Possible spawned entity movement types to compose
/// </summary>
public enum ShootType {
    ShootAtTarget,
    ShootInDirectionUp,
    ShootInDirectionDown,
    ShootInDirectionLeft,
    ShootInDirectionRight,
}

public class SpawnWave : Spawn {

    public float xSpeed;
    public float xMin;
    public float xMax;

    public float ySpeed;

    public List<MovementType> MovementBehaviors;

    public List<ShootType> ShootBehaviors;

    public GameObject bullet;

    public float spawnFrequency;

    private float runningTime;
    
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



    GameObject spawnEnemy() {
        
        // create the object
        GameObject newTarget = (GameObject)GameObject.CreatePrimitive(PrimitiveType.Cube);
        newTarget.transform.position = transform.position;
        newTarget.transform.rotation = transform.rotation;
        
        newTarget.name = "New Enemy";
        
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
        
        foreach (MovementType MoveBehav in MovementBehaviors) {
            switch (MoveBehav) {
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

                case MovementType.MoveToRandomPointY:
                    newTarget.gameObject.AddComponent<MoveToRandomPointY>();
                    newTarget.gameObject.GetComponent<MoveToRandomPointY>().moveRate = ySpeed;
                    newTarget.gameObject.GetComponent<MoveToRandomPointY>().moveBounds = background;
                    break;

                case MovementType.MoveInDirectionUp:
                    // add movement along Y axis behavior
                    newTarget.gameObject.AddComponent<MoveInDirection>();
                    newTarget.gameObject.GetComponent<MoveInDirection>().moveRate = ySpeed;
                    newTarget.gameObject.GetComponent<MoveInDirection>().dir = MoveDirection.Up;
                    break;
                case MovementType.MoveInDirectionDown:
                    // add movement along Y axis behavior
                    newTarget.gameObject.AddComponent<MoveInDirection>();
                    newTarget.gameObject.GetComponent<MoveInDirection>().moveRate = ySpeed;
                    newTarget.gameObject.GetComponent<MoveInDirection>().dir = MoveDirection.Down;
                    break;
                case MovementType.MoveInDirectionLeft:
                    // add movement along Y axis behavior
                    newTarget.gameObject.AddComponent<MoveInDirection>();
                    newTarget.gameObject.GetComponent<MoveInDirection>().moveRate = xSpeed;
                    newTarget.gameObject.GetComponent<MoveInDirection>().dir = MoveDirection.Left;
                    break;
                case MovementType.MoveInDirectionRight:
                    // add movement along Y axis behavior
                    newTarget.gameObject.AddComponent<MoveInDirection>();
                    newTarget.gameObject.GetComponent<MoveInDirection>().moveRate = xSpeed;
                    newTarget.gameObject.GetComponent<MoveInDirection>().dir = MoveDirection.Right;
                    break;

                default:
                    break;
            }
        }

        foreach (ShootType ShootBehav in ShootBehaviors) {
            switch (ShootBehav) {
                case ShootType.ShootAtTarget:
                    // add shooting behavior
                    newTarget.gameObject.AddComponent<NPCShootAtTarget>();
                    newTarget.gameObject.GetComponent<NPCShootAtTarget>().spawn = bullet;
                    newTarget.gameObject.GetComponent<NPCShootAtTarget>().bulletBounds = background;
                    newTarget.gameObject.GetComponent<NPCShootAtTarget>().bulletLife = 10.0f;
                    newTarget.gameObject.GetComponent<NPCShootAtTarget>().bulletSpeed = 5.0f;
                    newTarget.gameObject.GetComponent<NPCShootAtTarget>().frequency = 1.5f;
                    if (player != null) {
                        newTarget.gameObject.GetComponent<NPCShootAtTarget>().moveTarget = player.transform;
                    }
                    break;

                case ShootType.ShootInDirectionUp:
                    newTarget.gameObject.AddComponent<NPCShootInDirection>();
                    newTarget.gameObject.GetComponent<NPCShootInDirection>().spawn = bullet;
                    newTarget.gameObject.GetComponent<NPCShootInDirection>().bulletBounds = background;
                    newTarget.gameObject.GetComponent<NPCShootInDirection>().bulletLife = 10.0f;
                    newTarget.gameObject.GetComponent<NPCShootInDirection>().bulletSpeed = 5.0f;
                    newTarget.gameObject.GetComponent<NPCShootInDirection>().frequency = 1.5f;
                    newTarget.gameObject.GetComponent<NPCShootInDirection>().moveDir = MoveDirection.Up;
                    break;

                case ShootType.ShootInDirectionDown:
                    newTarget.gameObject.AddComponent<NPCShootInDirection>();
                    newTarget.gameObject.GetComponent<NPCShootInDirection>().spawn = bullet;
                    newTarget.gameObject.GetComponent<NPCShootInDirection>().bulletBounds = background;
                    newTarget.gameObject.GetComponent<NPCShootInDirection>().bulletLife = 10.0f;
                    newTarget.gameObject.GetComponent<NPCShootInDirection>().bulletSpeed = 5.0f;
                    newTarget.gameObject.GetComponent<NPCShootInDirection>().frequency = 1.5f;
                    newTarget.gameObject.GetComponent<NPCShootInDirection>().moveDir = MoveDirection.Down;
                    break;

                case ShootType.ShootInDirectionLeft:
                    newTarget.gameObject.AddComponent<NPCShootInDirection>();
                    newTarget.gameObject.GetComponent<NPCShootInDirection>().spawn = bullet;
                    newTarget.gameObject.GetComponent<NPCShootInDirection>().bulletBounds = background;
                    newTarget.gameObject.GetComponent<NPCShootInDirection>().bulletLife = 10.0f;
                    newTarget.gameObject.GetComponent<NPCShootInDirection>().bulletSpeed = 5.0f;
                    newTarget.gameObject.GetComponent<NPCShootInDirection>().frequency = 1.5f;
                    newTarget.gameObject.GetComponent<NPCShootInDirection>().moveDir = MoveDirection.Left;
                    break;

                case ShootType.ShootInDirectionRight:
                    newTarget.gameObject.AddComponent<NPCShootInDirection>();
                    newTarget.gameObject.GetComponent<NPCShootInDirection>().spawn = bullet;
                    newTarget.gameObject.GetComponent<NPCShootInDirection>().bulletBounds = background;
                    newTarget.gameObject.GetComponent<NPCShootInDirection>().bulletLife = 10.0f;
                    newTarget.gameObject.GetComponent<NPCShootInDirection>().bulletSpeed = 5.0f;
                    newTarget.gameObject.GetComponent<NPCShootInDirection>().frequency = 1.5f;
                    newTarget.gameObject.GetComponent<NPCShootInDirection>().moveDir = MoveDirection.Right;

                    // register this firing behavior with parameter manipulators
                    background.GetComponent<ParameterSliderBulletSpeed>().paramArray.Add(
                        newTarget.gameObject.GetComponent<NPCShootInDirection>());
                    background.GetComponent<ParameterSliderShootRate>().paramArray.Add(
                        newTarget.gameObject.GetComponent<NPCShootInDirection>());
                    break;

                default:
                    break;
            }
        }

        // add movement bounds to clean up
        newTarget.gameObject.AddComponent<CleanupBound>();
        newTarget.gameObject.GetComponent<CleanupBound>().boundingObject = background;
        
        return newTarget;
    }
}
