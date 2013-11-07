using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TutorialManager : MonoBehaviour {

    public List<DialogueBox> dialogueBoxes;
    public int currentBoxIndex;

	// Use this for initialization
	void Start () {
        GameState.Singleton.CurrentState = State.Paused;
	}
	
	// Update is called once per frame
	void Update () {
	}
}
