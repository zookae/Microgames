using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Score : MonoBehaviour {

    public List<string> goodTag;
    public List<string> badTag;

    public float gainPoints;
    public float losePoints;

    public float score = 0.0f;

    public GameObject GameObj;
    private GameState GameStatus;

    void Awake() {
        GameStatus = GameObj.GetComponent<GameState>();
    }

    void OnTriggerEnter(Collider col) {
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
}
