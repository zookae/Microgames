﻿using UnityEngine;
using System.Collections;
using System.Xml;
using System;

public class ButtonFireDirection : ShootInDirection {

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space)) {
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
    /// <param name="root">XML node of type "ButtonFireDirection"</param>
    public static void ParseFromXML(XmlNode root) {
        //http://www.csharp-examples.net/xml-nodes-by-name/
        XmlNode targetNode = root.SelectSingleNode("target"); // find all node with target property; assumes only one

        Debug.Log("found target node: " + targetNode.InnerText);

        // assign component to GameObject in the environment that has matching name
        GameObject cO = GameObject.Find(targetNode.InnerText); // find target in the environment
        cO.gameObject.AddComponent<ButtonFireDirection>(); // already know this component because of type
        ButtonFireDirection cref = cO.gameObject.GetComponent<ButtonFireDirection>(); // cache for efficiency

        // TODO: implement by tag to simplify assigning to all if repeated?

        // look up and parse each attribute needed for script
        cref.spawn = GameObject.Find(root.SelectSingleNode("spawn").InnerText);
        cref.moveDir = (MoveDirection)Enum.Parse(typeof(MoveDirection), root.SelectSingleNode("moveDirection").InnerText);
        cref.bulletSpeed = float.Parse(root.SelectSingleNode("moveSpeed").InnerText);
    }
}
