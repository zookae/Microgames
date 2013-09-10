using UnityEngine;
using System.Collections;

public class AimerControl : MoveControl {

    public GameObject ballObj;

    private BallControl ballScript;

    public Vector3 patrolA;
    public Vector3 patrolB;

    private bool hasClicked = false;


    // always called, even if not used in script
    void Awake() {
        ballScript = ballObj.GetComponent<BallControl>(); // expensive to call
    }

	// only used when script used and updates
	void Start () {
        moveRate = 5.0f;
        moveTarget = patrolB;
	}
	
	// Update is called once per frame
	void Update () {
        if (isRunning) {
            if (Input.GetMouseButtonDown(0) && !hasClicked) {
                Debug.Log("left click");
                //mouseTarget = Input.mousePosition;

                ballScript.mouseTarget = Input.mousePosition;
                ballScript.moveTarget = transform.position;
                ballScript.hasTarget = true;

                Debug.Log("click time target : " + transform.position);

                hasClicked = true;
            }
        }
        
	}

}
