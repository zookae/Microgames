using UnityEngine;
using System.Collections;

public class TerminateTag : Terminate {

    /// <summary>
    /// Tag that triggers termination
    /// </summary>
    public string triggerTag;

    void OnCollisionEnter(Collision col) {
        //Debug.Log("collided");
        if (col.gameObject.tag == triggerTag) {
            GameState.Singleton.CurrentState = termState;
        }
    }

    void OnTriggerEnter(Collider col) {
        //Debug.Log("collided");
        if (col.gameObject.tag == triggerTag) {
            GameState.Singleton.CurrentState = termState;
        }
    }
}
