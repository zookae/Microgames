using UnityEngine;
using System.Collections;

public class AnimatedUV : MonoBehaviour {

    public Vector2 uvAnimationRate;
    public string textureName = "_MainTex";

    Vector2 uvOffset = Vector2.zero;

	// Use this for initialization
	void Update() {
        Debug.Log(uvAnimationRate * Time.deltaTime);
        uvOffset += (uvAnimationRate * Time.deltaTime);
        Debug.Log("uvOffset is: " + uvOffset);
        renderer.material.SetTextureOffset("_MainTex", uvOffset);
	}
}
