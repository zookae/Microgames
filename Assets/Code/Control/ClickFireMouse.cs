using UnityEngine;
using System.Collections;

using System.Xml;
using System;

public class ClickFireMouse : ShootAtPoint {

    public Camera cam;

    Vector3 p;

	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0)) {
            moveTarget = TargetLocation(Input.mousePosition);
            ShootAtTar();
        }
	}

    /// <summary>
    /// Return the coordinates of the point where the mouse clicked
    /// </summary>
    /// <param name="mousePos">Location on the screen where the mouse clicked</param>
    /// <returns>Vector3 giving world coordinates of mouse click</returns>
    public Vector3 TargetLocation(Vector3 mousePos) {

        // TODO: need to search plane of world, not just click point

        mousePos.z = 10; // need Z value for ScreenToWorldPoint to work
        Vector3 p = cam.ScreenToWorldPoint(mousePos);
        p.z = transform.position.z; // z axis to plane defined by player

        return p;
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
        cO.gameObject.AddComponent<ClickFireMouse>(); // already know this component because of type
        ClickFireMouse cref = cO.gameObject.GetComponent<ClickFireMouse>(); // cache for efficiency

        // look up and parse each attribute needed for script
        cref.cam = (Camera)GameObject.FindObjectOfType(typeof(Camera)); // find the camera
        cref.spawn = GameObject.Find(root.SelectSingleNode("spawn").InnerText);
        cref.moveSpeed = float.Parse(root.SelectSingleNode("moveSpeed").InnerText);
    }
}
