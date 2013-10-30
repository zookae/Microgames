using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Tracks a collection of objects and registers clicks.
/// TODO (kasiu): Should this be a component attached to the global state or a single object?
/// </summary>
public class ClickSelectParent : MonoBehaviour {

    void Start() {}
    
	// Update is called once per frame
	void Update () {
        if (GameState.Singleton.CurrentState == State.Running) { // make sure game isn't over
            if (Input.GetMouseButtonDown(0)) {
                Transform clickObj = ObjectClicked();
                //Debug.Log("clicked on : " + clickObj.name);
                //Debug.Log("parent object: " + clickObj.transform.parent.gameObject.name);

                // XXX (kasiu): Will obviously break for any other objects in the scene with parents.
                if (clickObj == null || clickObj.parent == null) {
                    Debug.Log("Invalid object.");
                    return;
                }

                if (AlreadyClicked(clickObj.parent.name)) {
                    Debug.Log("Already clicked.");
                    return;
                } 

                Triple<double, string, string> tagging =
                    new Triple<double, string, string>(GameState.Singleton.TimeUsed,
                        clickObj.parent.name, clickObj.name);
                GameState.Singleton.clickTrace.Add(tagging);
                ChangeColor(clickObj.transform.parent.gameObject, clickObj.renderer.material.color);
                // TODO (kasiu): Eventually store tagging in the DB
            }
        }
	}

    public Transform ObjectClicked() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        bool hadHit = Physics.Raycast(ray, out hit);
        if (hadHit) {
            return hit.collider.transform;
        }
        return null;
    }

    public void StoreTagging(string obj, string tag) {
        // store in the DB
    }

    public void ChangeColor(GameObject obj, Color newColor) {
        obj.renderer.material.color = newColor;
    }

    public bool AlreadyClicked(string name) {
        foreach (Triple<double, string, string> t in GameState.Singleton.clickTrace) {
            if (name == t.Second) {
                return true;
            }
        }
        return false;
    }
}
