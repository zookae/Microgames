using UnityEngine;
using System.Collections;

public class LoadSprite : MonoBehaviour {

    public Texture texture;
    public bool assignedTexture;

	// Use this for initialization
	void Start () {
        DebugConsole.Log("Texture name : " + texture.name);
        assignedTexture = false;
        renderer.material.mainTexture = texture;
	}
	
	// Update is called once per frame
	void Update () {
	}
}
