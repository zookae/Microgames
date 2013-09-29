using UnityEngine;
using System.Collections;

public class NPCShootInDirection : ShootInDirection {

    /// <summary>
    /// Seconds between NPC shots
    /// </summary>
    public float frequency;

    /// <summary>
    /// [optional] Boundary outside of which bullet will be destroyed
    /// </summary>
    public GameObject bulletBounds;

    /// <summary>
    /// [optional] Time after which bullet will be destroyed
    /// </summary>
    public float bulletLife;

    private float timeDelta;

    private Random rng = new Random();
	
	// Update is called once per frame
	void Update () {
        timeDelta += Time.deltaTime; // increment time since fired

        if (timeDelta > frequency) {

            // fire bullet
            if (bulletBounds != null) {
                ShootInDir(bulletBounds);
            } else {
                ShootInDir();
            }
            timeDelta = 0; // reset time since fired
        }
	}
}
