using UnityEngine;
using System.Collections;

public class LightToggle : MonoBehaviour {

    public GameObject GameObj;
    private GameState GameStatus;


	// Use this for initialization
	void Start () {
        GameStatus = GameObj.GetComponent<GameState>();
	}
	
	// Update is called once per frame
	void Update () {

        // toggle light dark on end
        if (GameStatus.CurrentState == State.Win) {
            this.light.color = Color.yellow;
        }

        if (GameStatus.CurrentState == State.Lose) {
            this.light.color = Color.red;
        }
	}
}
