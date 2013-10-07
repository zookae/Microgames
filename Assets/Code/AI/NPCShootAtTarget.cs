using UnityEngine;
using System.Collections;

public class NPCShootAtTarget : ShootAtTarget {

    private float timeDelta;

    private Random rng = new Random();
	
	// Update is called once per frame
	void Update () {
        timeDelta += Time.deltaTime; // increment time since fired

        if (timeDelta > frequency) {

            // fire bullet
            if (bulletBounds != null && bulletLife > 0) {
                ShootAtTar(bulletBounds, bulletLife);
            }
            if (bulletBounds != null) {
                ShootAtTar(bulletBounds);
            } else if (bulletLife > 0) {
                ShootAtTar(bulletLife);
            } else {
                ShootAtTar();
            }

            timeDelta = 0; // reset time since fired
        }
	}
}
