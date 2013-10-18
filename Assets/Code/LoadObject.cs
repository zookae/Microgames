using UnityEngine;
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

    public Dictionary<string, Texture> itemImageMap = new Dictionary<string, Texture>();
    private string header = "gwap_sprites/";

	// Use this for initialization
	void Start () {
        // TEMP (kasiu): Load the sprite mapping in. Relocate when needed.
        TextAsset mapfile = Resources.Load("itemImageMap") as TextAsset;
        if (mapfile != null) {
            string[] itemList = mapfile.text.Split('\n');
            foreach (string s in itemList) {
                string[] pair = s.Split(',');
                if (pair.Length == 2) {
                    string textureName = header + pair[1];
                    DebugConsole.Log("Loaded texture: " + textureName);
                    itemImageMap.Add(pair[0], Resources.Load(textureName) as Texture);
                }
            }
        }

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
                DebugConsole.Log("Attempting to attach textures.");
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
}
