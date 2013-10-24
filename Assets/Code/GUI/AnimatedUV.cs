using UnityEngine;
using System.Collections;

public class AnimatedUV : MonoBehaviour {

    public Vector2 uvAnimationRate;
    public string textureName = "_MainTex";

    Vector2 uvOffset = Vector2.zero;

	// Use this for initialization
	void Update() {
        uvOffset += (uvAnimationRate * Time.deltaTime);
        renderer.material.SetTextureOffset("_MainTex", uvOffset);
	}
}
