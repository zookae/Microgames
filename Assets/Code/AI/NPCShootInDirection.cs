using UnityEngine;
using System.Collections;

public class NPCShootInDirection : ShootInDirection, INPCShootBehavior {

    private float timeDelta;

    // TODO: randomized firing rate
    private Random rng = new Random();
	
	// Update is called once per frame
	void Update () {
        timeDelta += Time.deltaTime; // increment time since fired

        if (timeDelta > frequency) {

            // fire bullet
            if (bulletBounds != null && bulletLife > 0) {
                ShootInDir(bulletBounds, bulletLife);
            }
            if (bulletBounds != null) {
                ShootInDir(bulletBounds);
            } else if (bulletLife > 0) {
                ShootInDir(bulletLife);
            } else {
                ShootInDir();
            }

            timeDelta = 0; // reset time since fired
        }
	}
}
