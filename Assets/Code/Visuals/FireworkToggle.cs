using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ParticleSystem))]
public class FireworkToggle : MonoBehaviour {
    
    public State triggerState;

	// Update is called once per frame
	void Update () {
        if (GameState.Singleton.CurrentState == triggerState) {
            this.particleSystem.Play();
        }
	}
}
