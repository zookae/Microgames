using UnityEngine;
using System.Collections;

public class LoadSprite : MonoBehaviour {

    public Texture texture;
    public bool assignedTexture;

	// Use this for initialization
	void Start () {
        assignedTexture = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (!assignedTexture && texture != null) {
            renderer.material.mainTexture = texture;
            assignedTexture = true;
        }
	}
}
