//using UnityEngine;
//using System.Collections;
//using System.Xml;
//using System;
////using UnityEditor;
//
//public class LoadComponent : MonoBehaviour {
//
//    public TextAsset componentXml = null;
//
//	// Use this for initialization
//	void Awake () {
//        Debug.Log("reading xml: ");
//        readComponents();
//	}
//
//    private void readComponents() {
//
//        // loading
//        //TextAsset txt = (TextAsset)Resources.Load(componentXml.text, typeof(TextAsset));
//        XmlDocument xml = new XmlDocument();
//        xml.LoadXml(componentXml.text);
//
//        Debug.Log("raw text: " + componentXml.text);
//        
//        // reading
//        XmlNode root = xml.FirstChild;
//        
//        foreach (XmlNode node in root.ChildNodes) {
//
//            if (node.NodeType == XmlNodeType.Comment)
//                continue;
//
//            if (node.Attributes["cname"] != null) {
//                ParseComponent(node);
//            }
//            else if (node.Attributes["iname"] != null) {
//                ParseInstance(node);
//            }
//        }
//    }
//
//    /// <summary>
//    /// Convenience function for retrieving a prefab by its name
//    /// Assumes prefab is located in Prefabs folder and returns the associated GameObject
//    /// </summary>
//    /// <param name="prefabName">name of the prefab in the Assets/Prefabs folder to retrieve</param>
//    /// <returns></returns>
//    public static GameObject GetPrefabByName(string prefabName) {
//        prefabName = "Assets/Prefabs/" + prefabName + ".prefab";
//        Debug.Log("[GetPrefabByName] finding spawn of: " + prefabName);
////        UnityEngine.Object prefab = AssetDatabase.LoadAssetAtPath(prefabName, typeof(GameObject));
//        Debug.Log("[GetPrefabByName] got gameobject of: " + prefab);
//        return (GameObject)prefab;
//    }
//
//    public static GameObject ParseInstance(XmlNode node) {
//        GameObject spawnObj = GetPrefabByName(node.SelectSingleNode("asset").InnerText);
//
//        spawnObj.name = node.Attributes["iname"].InnerText;
//        
//        // if position exists assign the object to a place in the world
//        XmlNode positionNode = node.SelectSingleNode("position");
//        if (positionNode != null) {
//            Debug.Log("[ParseInstance] got position: " + positionNode.InnerText);
//            GameObject.Instantiate(spawnObj);
//            string[] coords = positionNode.InnerText.Split(',');
//            if (coords.Length == 3) {
//                Vector3 spawnPosition = Vector3.zero;
//                spawnPosition.x = float.Parse(coords[0]);
//                spawnPosition.y = float.Parse(coords[1]);
//                spawnPosition.z = float.Parse(coords[2]);
//                spawnObj.transform.position = spawnPosition;
//            }
//            else if (coords.Length == 2) {
//                Vector3 spawnPosition = Vector3.zero;
//                spawnPosition.x = float.Parse(coords[0]);
//                spawnPosition.y = float.Parse(coords[1]);
//                spawnPosition.z = -1; // assume default value
//                spawnObj.transform.position = spawnPosition;
//            }
//        }
//        return spawnObj;
//    }
//
//    public void ParseComponent(XmlNode node) {
//        // parse each node according to type
//        switch (node.Attributes["cname"].Value) {
//            // controls for firing
//            case "ClickFireDirection":
//                Debug.Log("parsing a ClickFireDirection component");
//                ClickFireDirection.ParseFromXML(node);
//                break;
//            case "ClickFireMouse":
//                Debug.Log("parsing a ClickFireMouse component");
//                ClickFireMouse.ParseFromXML(node);
//                break;
//            case "ClickFireObject":
//                Debug.Log("parsing a ClickFireObject component");
//                ClickFireObject.ParseFromXML(node);
//                break;
//
//            // controls for movement
//            case "MoveByKeyForce":
//                Debug.Log("parsing a MoveByKeyForce component");
//                MoveByKeyForce.ParseFromXML(node);
//                break;
//
//            // movement behavior
//            case "MoveInDirection":
//                Debug.Log("parsing a MoveInDirection component");
//                MoveInDirection.ParseFromXML(node);
//                break;
//
//
//
//            // oops
//            default:
//                Debug.Log("saw and ignored a component: " + node.Attributes["cname"].Value);
//                break;
//        }
//    }
//}
