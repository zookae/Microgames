using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScoreTriggerTag : MonoBehaviour {

    /// <summary>
    /// List of tags that are "good" / give score
    /// </summary>
    public List<string> tagSet;

    /// <summary>
    /// Points gained for colliding with good tag
    /// </summary>
    public float pointChange;

    
    void OnTriggerEnter(Collider col) {

        if (GameState.Singleton.CurrentState == State.Running) {
            Debug.Log("score : entered trigger");
            foreach (string t in tagSet) {
                if (col.CompareTag(t)) {
                    GameState.Singleton.score += pointChange;
                }
            }
        }
    }

}
