using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScoreTriggerTag : MonoBehaviour {

    

    public List<string> goodTag;
    public List<string> badTag;

    public float gainPoints;
    public float losePoints;

    void OnTriggerEnter(Collider col) {

        // NOTE : 
        if (GameState.Singleton.CurrentState == State.Running) {
            Debug.Log("score : entered trigger");
            foreach (string gt in goodTag) {
                if (col.CompareTag(gt)) {
                    GameState.Singleton.score += gainPoints;
                }
            }
            foreach (string bt in badTag) {
                if (col.CompareTag(bt)) {
                    GameState.Singleton.score -= losePoints;
                }
            }
        }
    }

}
