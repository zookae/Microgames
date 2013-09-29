using UnityEngine;
using System.Collections;

public class LightToggle : MonoBehaviour {

    public State gameState;
    public Color tintColor;

    // Update is called once per frame
	void Update () {
        // toggle light dark on end
        if (GameState.Singleton.CurrentState == gameState) {
            this.light.color = tintColor;
        }
	}
}
