using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MoveControl))]
public class MoveOnClick : MonoBehaviour {

    public bool hasClicked = false;

    private MoveControl[] moveControllers;

	void Start () {
        // Freeze object movement to start
        moveControllers = gameObject.GetComponents<MoveControl>();
        foreach (MoveControl mc in moveControllers) {
            mc.isRunning = false;
        }
	}
	
	void Update () {

        // allow movement on click
        if (!hasClicked && Input.GetMouseButtonDown(0)) {
            foreach (MoveControl mc in moveControllers) {
                mc.isRunning = true;
            }

            hasClicked = true;
        }
	}
}
