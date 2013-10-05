using UnityEngine;
using System.Collections;

public class DownloadModel : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log("started hitting sketchup");

        string dlPath = "./Assets/Resources/Downloads";
        string defaultDlPath = "C:/Users/Alex/Desktop";
        string term = "star";

        IEnumerator gs = GoogleSketchUp.aSyncSearchForModel(term, dlPath, defaultDlPath);
        while (gs.MoveNext()) {
            Debug.Log("got an item");
            //object tmp = gs.Current;
            GoogleSketchUp.unzip(dlPath + "/" + term + ".zip", dlPath);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
