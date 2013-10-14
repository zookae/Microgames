using UnityEngine;
using System.Collections;

public class LoadSprite : MonoBehaviour {

    public string textureName;
    private string lastTextureName;

	// Use this for initialization
	void Start () {
        lastTextureName = null;

        // HACK (kasiu): Cabbage name is happily hacked in right now.
        //if (textureName != null) {
        //    renderer.material.mainTexture = Resources.Load("textureName") as Texture;
        //    lastTextureName = textureName;
        //}
        renderer.material.mainTexture = Resources.Load("gwap_sprites/cabbage") as Texture;
	}
	
	// Update is called once per frame
	void Update () {
        //if (textureName != lastTextureName) {
        //    renderer.material.mainTexture = Resources.Load(textureName) as Texture;
        //}	
	}
}
