using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LikertChangeTagOnce : MonoBehaviour {

    /// <summary>
    /// Tag of entity that colliding with triggers change
    /// </summary>
    public string targetTag;

    /// <summary>
    /// Type of the resource to change
    /// </summary>
    public LikertScale rtype;

    private TrackLikert tracker;

    //List<string> possibleAnswers;
    
    /// <summary>
    /// Origin point to return to
    /// </summary>
    private Vector3 origin;
    public bool hasChanged = false;

    void Start() {
        origin = transform.position;

        if (tracker == null) {
            tracker = GameObject.Find("GlobalState").GetComponent<TrackLikert>();
        }
    }

    /// <summary>
    /// Update Likert when triggering tag, then disable option until reset
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerEnter(Collider col) {
        //Debug.Log("resource loss : entered trigger");

        if (col.CompareTag(targetTag) && !hasChanged) {
            tracker.Increment(col.name, rtype);

            hasChanged = true;
        }
    }


    void Update() {
        if (Mathf.Abs(Vector3.Distance(transform.position, origin)) < 0.1) {
            hasChanged = false;
        }
    }
}
