using UnityEngine;
using System.Collections;

public class DestroyTime : DestroyBehavior {

    public float lifetimer;
	
	// Update is called once per frame
	void Update () {
        lifetimer -= Time.deltaTime;

        if (lifetimer < 0.0f) {
            Destroy(gameObject);
        }
	}
}
