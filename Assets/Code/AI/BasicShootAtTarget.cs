﻿using UnityEngine;
using System.Collections;

public class BasicShootAtTarget : MonoBehaviour {

    /// <summary>
    /// Name of object for NPC to fire toward
    /// </summary>
    public string targetTag;

    /// <summary>
    /// Frequency to poll environment for new objects
    /// </summary>
    public float senseFrequency;

    /// <summary>
    /// Minimum distance to consider taking avoid action
    /// </summary>
    public float minActDistance;

    /// <summary>
    /// Minimum time between shots fired
    /// </summary>
    public float minFireSpacing;

    /// <summary>
    /// Speed to move when seeking toward alignment
    /// </summary>
    public float moveSpeed;

    public GameObject[] targetObjects;

    public float actDelta;
    public float senseDelta;

    private ShootInDirection shootBehav;
    public GameObject spawnee;

    public GameObject curTarget;
    float targetDistance = Mathf.Infinity;
    float newTargetDistance = Mathf.Infinity;

	// Use this for initialization
	void Start () {
        shootBehav = this.gameObject.AddComponent<ShootInDirection>();
        shootBehav.moveDir = MoveDirection.Up;
        shootBehav.bulletSpeed = 5.0f;
        shootBehav.spawn = spawnee;
	}
	
	// Update is called once per frame
	void Update () {
	    // start by assuming shooting down
        // then generalize by inferring cardinal direction and firing that way
        // or generalize by shooting along vector to target
        senseDelta += Time.deltaTime; // track time since last sensing
        actDelta += Time.deltaTime;

        // sensing
        if (senseDelta > senseFrequency) {

            // pick nearest target
            targetObjects = GameObject.FindGameObjectsWithTag(targetTag);
            foreach (GameObject go in targetObjects) {
                if (go == null)
                    continue;
                newTargetDistance = Vector3.Distance(transform.position, go.transform.position); // distance to object

                // update target if something else is nearer
                if (newTargetDistance < targetDistance) {
                    targetDistance = newTargetDistance;
                    curTarget = go;
                }
            }

            senseDelta = 0.0f;
        }


        // acting
        if (actDelta > minFireSpacing) {

            // test for alignment
            if (curTarget == null)
                return;
            float xDist = transform.position.x - curTarget.transform.position.x;
            if (Mathf.Abs(xDist) > minActDistance) {
                // seek to align
                transform.position = Vector3.MoveTowards(transform.position,
                    transform.position - new Vector3(xDist, 0, 0),
                    moveSpeed * Time.deltaTime);
            }
            else {
                // shoot in direction when aligned
                this.shootBehav.ShootInDir();
            }

            actDelta = 0.0f;
        }
            

        

	}
}
