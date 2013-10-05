using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScoreTriggerTagSet : MonoBehaviour {

    /// <summary>
    /// List of tags that are "good" / give score
    /// </summary>
    public List<string> goodTag;

    /// <summary>
    /// List of tags that are "bad" / lose score
    /// </summary>
    public List<string> badTag;

    /// <summary>
    /// Points gained for colliding with good tag
    /// </summary>
    public float gainPoints;

    /// <summary>
    /// Points lost for colliding with bad tag
    /// </summary>
    public float losePoints;

    void OnTriggerEnter(Collider col) {

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
