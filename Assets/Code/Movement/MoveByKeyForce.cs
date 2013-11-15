using UnityEngine;
using System;
using System.Xml;
using System.Collections;


public class MoveByKeyForce : MonoBehaviour {

    private Rigidbody rbody;
    public float force;
    public float drag=0.0f;

    public Vector3 maxVelocity;

	// Use this for initialization
	void Awake () {
        gameObject.AddComponent<Rigidbody>();
        rbody = gameObject.GetComponent<Rigidbody>();
        rbody.useGravity = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        MoveByForceClamped(); // apply force from key press
        MoveDrag(); // apply drag
	}


    void MoveByForce() {
        float xMovement = Input.GetAxis("Horizontal");
        float yMovement = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(xMovement, yMovement, 0);
        rbody.AddForce(dir * force, ForceMode.Force);
    }

    void MoveByForceClamped() {
        MoveByForce();
        Vector3 clamp = new Vector3(0, 0, 0);
        clamp.x = Mathf.Clamp(rbody.velocity.x, -maxVelocity.x, maxVelocity.x);
        clamp.y = Mathf.Clamp(rbody.velocity.y, -maxVelocity.y, maxVelocity.y);
        rbody.velocity = clamp;
    }

    void MoveDrag() {
        Vector3 oppositeForce = drag * -rbody.velocity;
        rbody.AddRelativeForce(oppositeForce);
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

        cO.gameObject.AddComponent<MoveByKeyForce>(); // already know this component because of type
        MoveByKeyForce cref = cO.gameObject.GetComponent<MoveByKeyForce>(); // cache for efficiency

        

        // look up and parse each attribute needed for script
        cref.force = float.Parse(root.SelectSingleNode("moveForce").InnerText);
        cref.maxVelocity = Vector3FromString(root.SelectSingleNode("maxVelocity").InnerText);
    }

    /// <summary>
    /// Convert a string of form "float,float,float" to a Vector3
    /// </summary>
    /// <param name="str">Comma-separated string of form "float,float,float"</param>
    /// <returns>Vector3 of the parsed float values</returns>
    private static Vector3 Vector3FromString(string str) {
        string[] strParts = str.Split(',');
        if (strParts.Length != 3) {
            Debug.Log("invalid string, has more than 3 comma-separated parts");
            return Vector3.zero;
        }
        return new Vector3(
            float.Parse(strParts[0]), 
            float.Parse(strParts[1]), 
            float.Parse(strParts[2]));
    }
}
