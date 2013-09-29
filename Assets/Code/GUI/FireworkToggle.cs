using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ParticleSystem))]
public class FireworkToggle : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        if (GameState.Singleton.CurrentState == State.Win) {
            this.particleSystem.Play();
        }
	}
}
