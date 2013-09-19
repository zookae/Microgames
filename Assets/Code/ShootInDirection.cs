using UnityEngine;
using System.Collections;

public class ShootInDirection : Spawn {

    /// <summary>
    /// Speed for shot object to move
    /// </summary>
    public float moveSpeed;

    /// <summary>
    /// Direction for bullet to move in
    /// </summary>
    public MoveDirection moveDir;

	/// <summary>
	/// Create a bullet and set it to move in a given direction
	/// </summary>
    public GameObject ShootInDir() {
        Debug.Log("called ShootInDir");
        GameObject bullet = SpawnTriggerable();
        bullet.AddComponent<MoveInDirection>();
        bullet.GetComponent<MoveInDirection>().dir = moveDir;
        bullet.GetComponent<MoveInDirection>().moveRate = moveSpeed;

        return bullet;
    }
}
