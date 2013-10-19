﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoadObject : MonoBehaviour {

    /// <summary>
    /// Prefab for the objects to spawn
    /// </summary>
    public GameObject prefab;

    /// <summary>
    /// List of object names to instantiate
    /// </summary>
    public List<string> objectNames = new List<string>();

    /// <summary>
    /// List of tag labels to assign to object selectors
    /// </summary>
    public List<string> tagNames = new List<string>();

    public List<Vector3> layoutPositions = new List<Vector3>();

    /// <summary>
    /// Currently expected a comma-delimeted .txt file.
    /// TODO (kasiu): Change this to something smarter.
    /// </summary>
    public TextAsset objectSpriteMapping;

    private const string header = "gwap_sprites/";

    /// <summary>
    /// The object-(loaded texture) mapping (stored from load).
    /// </summary>
    public Dictionary<string, Texture> itemImageMap = new Dictionary<string, Texture>();

	// Use this for initialization
	void Start () {
        LoadMapping();

        int layoutCounter = 0;
        foreach (string objName in objectNames) {
            // TODO: smarter layout choices OR fixed grid
            Vector3 position = GenerateRandomPosition(layoutPositions[layoutCounter]);
            GameObject newObject = (GameObject)GameObject.Instantiate(prefab,
                position, Quaternion.identity);
            newObject.AddComponent("ScoreTriggerTagAgreement");

            newObject.name = objName;
            newObject.transform.FindChild("Tag1").name = tagNames[0];
            newObject.transform.FindChild("Tag2").name = tagNames[1];
            if (newObject.transform.FindChild("Tag3") != null) {
                newObject.transform.FindChild("Tag3").name = tagNames[2];
            }

            Transform spriteChild = newObject.transform.FindChild("TexturedQuad");
            if (spriteChild != null) {
                DebugConsole.Log("Attempting to attach textures: " + objName);
                spriteChild.gameObject.AddComponent("LoadSprite");
                LoadSprite spriteComponent = spriteChild.GetComponent<LoadSprite>();
                spriteComponent.texture = itemImageMap[objName];
            }
            layoutCounter++;
        }
	}

    private const float maxRandX = 0.01f;
    private const float maxRandY = 0.02f;

    private Vector3 GenerateRandomPosition(Vector3 position) {
        Vector3 newPosition = position;
        float randX = Random.Range(-maxRandX, maxRandX);
        float randY = Random.Range(-maxRandY, maxRandY);
        position.x += randX;
        position.y += randY;
        return position;
    }

    private void LoadMapping() {
        if (objectSpriteMapping != null) {
            string[] itemList = objectSpriteMapping.text.Split('\n');
            foreach (string s in itemList) {
                string[] pair = s.Split(',');
                if (pair.Length == 2) {
                    DebugConsole.Log("For item " + pair[0] + " we're loading: " + textureName);
                    string textureName = (header + pair[1]).Trim() ;
                    Texture texture = Resources.Load(textureName) as Texture;
                    if (texture != null) {
                        itemImageMap.Add(pair[0], texture);
                    }
                }
            }
        }
    }
}
