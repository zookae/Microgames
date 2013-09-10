using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ParticleSystem))]
public class FireworkToggle : MonoBehaviour {

    public GameObject GameObj;
    private GameState GameStatus;


	// Use this for initialization
	void Start () {
        GameStatus = GameObj.GetComponent<GameState>();
	}
	
	// Update is called once per frame
	void Update () {
        if (GameStatus.CurrentState == State.Win) {
            this.particleSystem.Play();
        }
	}
}
