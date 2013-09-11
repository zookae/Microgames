using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Score : MonoBehaviour {

    public List<string> goodTag;
    public List<string> badTag;

    public float gainPoints;
    public float losePoints;

    public float score = 0.0f;

    void OnTriggerEnter(Collider col) {

        // NOTE : 
        if (GameState.Singleton.CurrentState == State.Running) {
            Debug.Log("score : entered trigger");
            foreach (string gt in goodTag) {
                if (col.CompareTag(gt)) {
                    score += gainPoints;
                }
            }
            foreach (string bt in badTag) {
                if (col.CompareTag(bt)) {
                    score -= losePoints;
                }
            }
        }
    }
	
	/// <summary>
	/// Draws score on screen
	/// </summary>
	void OnGUI() {
		GUI.Box(new Rect(10, 10, 100, 20), "score : "+score);
	}
}
