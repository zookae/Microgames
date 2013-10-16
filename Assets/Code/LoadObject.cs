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

	// Use this for initialization
	void Start () {
        //int layoutCounter = 0;
        //foreach (string objName in objectNames) {
        //    // TODO: smarter layout choices OR fixed grid
        //    Vector3 position = GenerateRandomPosition(layoutPositions[layoutCounter]);
        //    GameObject newObject = (GameObject)GameObject.Instantiate(prefab, 
        //        position, Quaternion.identity);
        //    newObject.AddComponent("ScoreTriggerTagAgreement");

        //    newObject.name = objName;
        //    newObject.transform.FindChild("Tag1").name = tagNames[0];
        //    //newObject.transform.FindChild("Tag1Label").guiText.text = tagNames[0];
        //    newObject.transform.FindChild("Tag2").name = tagNames[1];
        //    if (newObject.transform.FindChild("Tag3") != null) {
        //        newObject.transform.FindChild("Tag3").name = tagNames[2];
        //    }
        //    layoutCounter++;
        //}
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
