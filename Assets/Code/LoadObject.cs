using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoadObject : MonoBehaviour {

    /// <summary>
    /// Prefab for the objects to spawn
    /// </summary>
    public GameObject prefab;

    // list of objects
    public List<string> objectNames = new List<string>();

    // 2 tags
    public List<string> tagNames = new List<string>();

	// Use this for initialization
	void Start () {
        foreach (string objName in objectNames) {
            // TODO: smarter layout choices OR fixed grid
            GameObject newObject = (GameObject)GameObject.Instantiate(prefab, 
                transform.position, transform.rotation);

            newObject.name = objName;
            newObject.transform.FindChild("Tag1").name = tagNames[0];
            newObject.transform.FindChild("Tag2").name = tagNames[1];
            if (newObject.transform.FindChild("Tag3") != null) {
                newObject.transform.FindChild("Tag3").name = tagNames[2];
            }
        }
	}
}
