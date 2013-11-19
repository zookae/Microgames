using UnityEngine;
using System.Collections;
using System.Xml;
using System;

[RequireComponent(typeof(Rigidbody))]
public class MoveInDirection : MoveControl {

    public MoveDirection dir;

	// Update is called once per frame
	void Update () {
        if (isRunning) {
            switch (dir) {
                case MoveDirection.Up:
                    //this.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.up * moveRate);
                    transform.position += transform.up * moveRate * Time.deltaTime;
                    break;
                case MoveDirection.Down:
                    //this.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.down * moveRate);
                    transform.position -= transform.up * moveRate * Time.deltaTime;
                    break;
                case MoveDirection.Right:
                    //this.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.right * moveRate);
                    transform.position += transform.right * moveRate * Time.deltaTime;
                    break;
                case MoveDirection.Left:
                    //this.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.left * moveRate);
                    transform.position -= transform.right * moveRate * Time.deltaTime;
                    break;
            }
        }
	}

    public void MoveInDirectionM(MoveDirection dir, float moveSpeed) {
        if (isRunning) {
            switch (dir) {
                case MoveDirection.Up:
                    //this.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.up * moveRate);
                    transform.position += transform.up * moveRate * Time.deltaTime;
                    break;
                case MoveDirection.Down:
                    //this.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.down * moveRate);
                    transform.position -= transform.up * moveRate * Time.deltaTime;
                    break;
                case MoveDirection.Right:
                    //this.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.right * moveRate);
                    transform.position += transform.right * moveRate * Time.deltaTime;
                    break;
                case MoveDirection.Left:
                    //this.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.left * moveRate);
                    transform.position -= transform.right * moveRate * Time.deltaTime;
                    break;
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
        if (cO == null)
            return;
        cO.gameObject.AddComponent<MoveInDirection>(); // already know this component because of type
        MoveInDirection cref = cO.gameObject.GetComponent<MoveInDirection>(); // cache for efficiency

        // look up and parse each attribute needed for script
        cref.dir = (MoveDirection)Enum.Parse(typeof(MoveDirection), root.SelectSingleNode("moveDirection").InnerText);
        cref.moveRate = float.Parse(root.SelectSingleNode("moveSpeed").InnerText);
    }
}
