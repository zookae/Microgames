using UnityEngine;
using System.Collections;

public class ClickSelectParent : MonoBehaviour {

    /// <summary>
    /// Object clicked on
    /// </summary>
    private Transform clickObj;

    /// <summary>
    /// Color to change the tagged object on clicking
    /// </summary>
    //public Color clickedColor;

    /// <summary>
    /// Score lost if clicking on something formerly blocked
    /// </summary>
    public float scoreBlocked = 10;

    /// <summary>
    /// Score gained if clicking on something to label
    /// </summary>
    public float scoreLabeled = 10;

    void Start() {
        GameState.Singleton.partnerTrace.Add(new Triple<double, string, string>(0.0f, "Object-Cabbage", "Tag1"));
    }
    
	// Update is called once per frame
	void Update () {
        if (GameState.Singleton.CurrentState == State.Running) { // make sure game isn't over
            if (Input.GetMouseButtonDown(0)) {
                ObjectClicked();
                //Debug.Log("clicked on : " + clickObj.name);
                //Debug.Log("parent object: " + clickObj.transform.parent.gameObject.name);

                // triples of: (time, object, tag)

                // iterate over all objects with interaction history and check for the clicked object parent
                bool wasBlocked = false;
                foreach (Triple<double, string, string> click in GameState.Singleton.partnerTrace) {
                    if (click.Second == clickObj.parent.name && 
                        click.First < GameState.Singleton.TimeUsed &&
                        GameState.Singleton.blockTags.Contains(click.Third) &&
                        GameState.Singleton.labelTags.Contains(clickObj.name)) {
                        wasBlocked = true;
                        break;
                    }
                }

                // iterate over all objects with interaction history and check for the clicked object parent
                bool hasKey = false;
                foreach (Triple<double, string, string> click in GameState.Singleton.clickTrace) {
                    if (click.Second == clickObj.parent.name) {
                        hasKey = true;
                        break;
                    }
                }

                // if not yet clicked, change the parent object color and register the click
                if (!hasKey) {
                    ChangeColor(clickObj.transform.parent.gameObject, clickObj.renderer.material.color);

                    Triple<double, string, string> tagging =
                        new Triple<double, string, string>(GameState.Singleton.TimeUsed,
                            clickObj.parent.name, clickObj.name);
                    GameState.Singleton.clickTrace.Add(tagging);
                    // TODO: convert me to time since game start
                    Debug.Log("click trace: " + GameState.Singleton.TimeUsed + "," +
                            clickObj.parent.name + "," + clickObj.name);


                    if (wasBlocked) {
                        Debug.Log("docking score: " + scoreBlocked);
                        GameState.Singleton.score -= scoreBlocked;
                    } else {
                        Debug.Log("adding score: " + scoreLabeled);
                        GameState.Singleton.score += scoreLabeled;
                    }
                }
            }
        }
	}

    void ObjectClicked() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        bool hadHit = Physics.Raycast(ray, out hit);
        if (hadHit) {
            clickObj = hit.collider.transform;
        }
    }

    public void StoreTagging(string obj, string tag) {
        // store in the DB
    }

    public void ChangeColor(GameObject obj, Color newColor) {
        obj.renderer.material.color = newColor;
    }
}
