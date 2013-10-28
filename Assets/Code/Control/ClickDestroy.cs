using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClickDestroy : MonoBehaviour {
    /// <summary>
    /// Object clicked on
    /// </summary>
    private Transform clickObj;

    public List<string> targetTags;

	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0)) {
            ObjectClicked();
            //Debug.Log("clicked on : " + clickObj.name);
            foreach (string tag in targetTags) {
                //Debug.Log("destroy tags: " + tag);
                if (clickObj.CompareTag(tag)) {
                    Destroy(clickObj.gameObject);
                }
            }
            //Debug.Log("object tag: " + clickObj.tag);
            
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
}

