using UnityEngine;
using System.Collections;

public class Termination : MonoBehaviour {

    public GameObject GameObj;
    private GameState GameStatus;
	public static float maxTime = 7;

    void Awake() {
        GameStatus = GameObj.GetComponent<GameState>();
    }
	
	void Update() {
        if (GameStatus.TimeUsed > maxTime && GameStatus.CurrentState == State.Running) {
			GameStatus.CurrentState = State.Lose;
			DisableRunning();
		}
	}

    void OnCollisionEnter(Collision col) {
        Debug.Log("collided");
        if (col.gameObject.name == "Blocker") {
            Debug.Log("FAILURE!");
            GameStatus.CurrentState = State.Lose;
            DisableRunning();
        }
    }


    // TODO probably better to have objects with "Goal" tag to allow many targets
    void OnTriggerEnter(Collider otherObj) {
        Debug.Log("triggered");
        if (otherObj.gameObject.name == "Goal") {
            Debug.Log("GOOOAAAAAL!");
            GameStatus.CurrentState = State.Win;
            DisableRunning();
        } else if (otherObj.gameObject.name == "Blocker") {
            Debug.Log("FAILURE!");
            GameStatus.CurrentState = State.Lose;
            DisableRunning();
        }


        if (otherObj.gameObject.name == "Target") {
            Debug.Log("GOOOAAAAAL!");
        }
        if (otherObj.gameObject.name == "Target(Clone)") {
            Debug.Log("clone goal!");
        }
        if (otherObj.CompareTag("Target")) {
            Debug.Log("TAG GOAL!"); // TODO: must add tag name in Tag manager --> programmatically?
        }
        if (otherObj.CompareTag("Blocker")) {
            Debug.Log("TAG BLOCK!"); // TODO: must add tag name in Tag manager --> programmatically?
        }
    }

    /// <summary>
    /// Globally swap all MoveControl objects to have their "isRunning" flag disabled
    /// </summary>
    void DisableRunning() {
        object[] allObjects = Resources.FindObjectsOfTypeAll(typeof(MoveControl));
        foreach (object thisObject in allObjects) {
            MoveControl tarObj = (MoveControl)thisObject;
            tarObj.isRunning = false;
        }
    }
}
