using UnityEngine;
using System.Collections;
using System.Xml;
using System;

public class ClickFireDirection : ShootInDirection {

    /// <summary>
    /// [optional] Boundary to destroy projectile if it passes.
    /// </summary>
    public GameObject bulletBounds;

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            if (bulletBounds != null) {
                ShootInDir(bulletBounds);
            } else {
                ShootInDir();
            }
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
        cO.gameObject.AddComponent<ClickFireDirection>(); // already know this component because of type
        ClickFireDirection cref = cO.gameObject.GetComponent<ClickFireDirection>(); // cache for efficiency

        // TODO: implement by tag to simplify assigning to all if repeated?

        // look up and parse each attribute needed for script
        cref.spawn = GameObject.Find(root.SelectSingleNode("spawn").InnerText);
        cref.moveDir = (MoveDirection)Enum.Parse(typeof(MoveDirection), root.SelectSingleNode("moveDirection").InnerText);
        cref.moveSpeed = float.Parse(root.SelectSingleNode("moveSpeed").InnerText);
    }
}
