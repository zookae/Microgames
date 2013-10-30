using UnityEngine;
using System.Collections;

public class LaunchAtAimer : MoveControl {

    /// <summary>
    /// Target aimed at for movement when launching
    /// </summary>
    public Transform AimTarget;

    private MoveToPoint aim;
    private MoveOnClick click;

	// Use this for initialization
	void Start () {
        gameObject.AddComponent<MoveToPoint>();
        aim = gameObject.GetComponent<MoveToPoint>(); // object tracked for aiming
        aim.moveRate = this.moveRate;

        click = gameObject.AddComponent<MoveOnClick>();
	}
	
	// Update is called once per frame
	void Update () {
        if (GameState.Singleton.CurrentState != State.Running)
            return;

        // only track position updates before launch
        if (!click.hasClicked) {
            aim.movementPoint = AimTarget.position; // update target position
        }
	    
	}
}
