using UnityEngine;
using System.Collections;

public class SpaceToroid : MonoBehaviour {

    //public GameObject boundingObject;

    //private Vector3 transsize;
    //private Bounds boundary;
    private Vector3 otherSide;
    private Camera mainCam;

    public Vector3 screenPosition;

    void Awake() {
        mainCam = Camera.main;
    }

	// Use this for initialization
    //void Start() {
    //    boundary = boundingObject.transform.collider.bounds; // cache to save some computation
    //    transsize = transform.collider.bounds.size; // cache to save some computation
    //}
	
	// Update is called once per frame
	void Update () {

        // TODO: use camera 
        screenPosition = mainCam.WorldToScreenPoint(transform.position);
        if (screenPosition.x < 0) {
            //Debug.Log(transform.name + " is left of X bounds of screen");
            //Debug.Log("starting screen position: " + screenPosition);
            otherSide = screenPosition;
            otherSide.x = Screen.width - 1;
            //Debug.Log("target screen position: " + otherSide);
            otherSide = mainCam.ScreenToWorldPoint(otherSide);
            //Debug.Log("target world position: " + otherSide);
            transform.position = otherSide;
        }
        else if (screenPosition.x > Screen.width) {
            //Debug.Log(transform.name + " is right of X bounds of screen");
            //Debug.Log("starting screen position: " + screenPosition);
            otherSide = screenPosition;
            otherSide.x = 1;
            //Debug.Log("target screen position: " + otherSide);
            otherSide = mainCam.ScreenToWorldPoint(otherSide);
            //Debug.Log("target world position: " + otherSide);
            transform.position = otherSide;
        }
        else if (screenPosition.y < 0) {
            //Debug.Log(transform.name + " is below Y bounds of screen");
            //Debug.Log("starting screen position: " + screenPosition);
            otherSide = screenPosition;
            otherSide.y = Screen.height - 1;
            //Debug.Log("target screen position: " + otherSide);
            otherSide = mainCam.ScreenToWorldPoint(otherSide);
            //Debug.Log("target world position: " + otherSide);
            transform.position = otherSide;
        }
        else if (screenPosition.y > Screen.height) {
            //Debug.Log(transform.name + " is above Y bounds of screen");
            //Debug.Log("starting screen position: " + screenPosition);
            otherSide = screenPosition;
            otherSide.y = 1;
            //Debug.Log("target screen position: " + otherSide);
            otherSide = mainCam.ScreenToWorldPoint(otherSide);
            //Debug.Log("target world position: " + otherSide);
            transform.position = otherSide;
        }

        // test for exceeding bounds on left
        //if (transform.position.x + transsize.x < boundary.min.x) {
        //    Debug.Log(transform.name + " is left of X bounds of " + boundingObject.name);
        //    otherSide = transform.position;
        //    otherSide.x = boundary.max.x + transsize.x;
        //    transform.position = otherSide;
        //}
        //    // test for exceeding bounds on right
        //else if (transform.position.x - transsize.x > boundary.max.x) {
        //    Debug.Log(transform.name + " is right of X bounds of " + boundingObject.name);
        //    otherSide = transform.position;
        //    otherSide.x = boundary.min.x - transsize.x;
        //    transform.position = otherSide;
        //}
        //    // test for exceeding bounds on top
        //else if (transform.position.y - transsize.y > boundary.max.y) {
        //    Debug.Log(transform.name + " is above Y bounds of " + boundingObject.name);
        //    otherSide = transform.position;
        //    otherSide.y = boundary.min.y - transsize.y;
        //    transform.position = otherSide;
        //}
        //    // test for exceeding bounds on bottom
        //else if (transform.position.y + transsize.y < boundary.min.y) {
        //    Debug.Log(transform.name + " is below Y bounds of " + boundingObject.name);
        //    otherSide = transform.position;
        //    otherSide.y = boundary.max.y + transsize.y;
        //    transform.position = otherSide;
        //} 
	}

}
