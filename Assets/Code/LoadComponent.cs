using UnityEngine;
using System.Collections;
using System.Xml;
using System;

public class LoadComponent : MonoBehaviour {

    public TextAsset componentXml = null;

	// Use this for initialization
	void Start () {
        Debug.Log("reading xml: ");
        readComponents();
	}

    private void readComponents() {

        // loading
        //TextAsset txt = (TextAsset)Resources.Load(componentXml.text, typeof(TextAsset));
        XmlDocument xml = new XmlDocument();
        xml.LoadXml(componentXml.text);

        Debug.Log("raw text: " + componentXml.text);
        
        // reading
        XmlNode root = xml.FirstChild;
        
        foreach (XmlNode node in root.ChildNodes) {
            // skip if node has no name
            if (node.NodeType == XmlNodeType.Comment || node.Attributes["cname"] == null) {
                continue;
            }

            // parse each node according to type
            switch (node.Attributes["cname"].Value) {
                // controls for firing
                case "ClickFireDirection":
                    Debug.Log("parsing a ClickFireDirection component");
                    ClickFireDirection.ParseFromXML(node);
                    break;
                case "ClickFireMouse":
                    Debug.Log("parsing a ClickFireMouse component");
                    ClickFireMouse.ParseFromXML(node);
                    break;
                case "ClickFireObject":
                    Debug.Log("parsing a ClickFireObject component");
                    ClickFireObject.ParseFromXML(node);
                    break;

                // controls for movement
                case "MoveByKeyForce":
                    Debug.Log("parsing a MoveByKeyForce component");
                    MoveByKeyForce.ParseFromXML(node);
                    break;

                // movement behavior
                case "MoveInDirection":
                    Debug.Log("parsing a MoveInDirection component");
                    MoveInDirection.ParseFromXML(node);
                    break;



                    // oops
                default:
                    Debug.Log("saw and ignored a " + node.Attributes["cname"].Value + " component");
                    break;
            }
        }
    }
}
