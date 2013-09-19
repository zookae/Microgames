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
        Debug.Log("loading xml");

        // loading
        //TextAsset txt = (TextAsset)Resources.Load(componentXml.text, typeof(TextAsset));
        XmlDocument xml = new XmlDocument();
        xml.LoadXml(componentXml.text);

        Debug.Log("raw text: " + componentXml.text);

        Debug.Log("reading xml");

        // reading
        XmlNode root = xml.FirstChild;

        gameObject.AddComponent<ClickFireDirection>();
        ClickFireDirection cft = gameObject.GetComponent<ClickFireDirection>();
        cft.spawn = GameObject.Find("Bullet");
        cft.moveDir = MoveDirection.Down;

        foreach (XmlNode node in root.ChildNodes) {
            if (node.NodeType == XmlNodeType.Element) {
                Debug.Log(node.Name + " : " + node.InnerText);
                switch(node.Name) {
                    //case "spawn":
                    //    Debug.Log("parsed spawn: " + node.InnerText);
                    //    cft.spawn = GameObject.Find(node.InnerText);
                    //    break;
                    case "moveDirection":
                        Debug.Log("parsed move direction: " + Enum.Parse(typeof(MoveDirection), node.InnerText));
                        cft.moveDir = (MoveDirection)Enum.Parse(typeof(MoveDirection), node.InnerText);
                        break;
                    case "moveSpeed":
                        Debug.Log("parsed move speed: " + float.Parse(node.InnerText));
                        cft.moveSpeed = float.Parse(node.InnerText);
                        break;
                }
            }
        }
    }
}
