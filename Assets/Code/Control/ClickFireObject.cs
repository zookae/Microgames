using UnityEngine;
using System.Collections;
using System.Xml;
using System;

public class ClickFireObject : ShootAtTarget {

    /// <summary>
    /// Object to fire at when clicking
    /// </summary>
    public GameObject fireTarget;

    void Start() {
        moveTarget = fireTarget.transform;
    }

	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0)) {
            moveTarget = fireTarget.transform;
            ShootAtTar();
        }
	}

    /// <summary>
    /// Given a root XML node parse all the children and 
    /// instantiate a component on the appropriate object with the appropriate parameters
    /// </summary>
    /// <param name="root">XML node of type "ClickFireDirection"</param>
    public static void ParseFromXML(XmlNode root) {
        //http://www.csharp-examples.net/xml-nodes-by-name/
        XmlNode targetNode = root.SelectSingleNode("target"); // find all node with target property; assumes only one

        Debug.Log("found target node: " + targetNode.InnerText);

        // assign component to GameObject in the environment that has matching name
        GameObject cO = GameObject.Find(targetNode.InnerText); // find target in the environment
        cO.gameObject.AddComponent<ClickFireObject>(); // already know this component because of type
        ClickFireObject cref = cO.gameObject.GetComponent<ClickFireObject>(); // cache for efficiency

        // look up and parse each attribute needed for script
        cref.spawn = GameObject.Find(root.SelectSingleNode("spawn").InnerText);
        cref.fireTarget = GameObject.Find(root.SelectSingleNode("fireTarget").InnerText);
        cref.bulletSpeed = float.Parse(root.SelectSingleNode("moveSpeed").InnerText);
    }
}
