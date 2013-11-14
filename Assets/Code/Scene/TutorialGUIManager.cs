using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TutorialGUIManager : MonoBehaviour {

    public string nextSceneName;
    public Vector2 position;
    public Vector2 windowDimensions;
    public Font font;
    public int fontSize;
    public Color fontColor;

    private GameObject tutorialStartObject;
    private GameObject tutorialEndObject;

    private bool tutorialSetup;

	// Use this for initialization
	void Start () {
        GameState.Singleton.CurrentState = State.Paused;
        tutorialSetup = false;
        //TutorialSetup();
	}
	
	// Update is called once per frame
	void Update () {
        // Setup tutorial
        if (this.gameObject.GetComponent<TutorialGWAPManager>() != null && this.gameObject.GetComponent<TutorialGWAPManager>().IsReadyToSpawn() && !tutorialSetup) {
            TutorialSetup();
        }
        // Triggers        
        if (tutorialStartObject != null && tutorialStartObject.GetComponent<TutorialGUI>().IsTutorialFinished()) {
            GameObject.Destroy(tutorialStartObject);
            tutorialStartObject = null; // don't know if this needs to be explicit, but eh
            // LAUNCH SOME ITEMS
            GameState.Singleton.CurrentState = State.Running;
        }

        if (tutorialEndObject == null && (GameState.Singleton.CurrentState == State.Win || GameState.Singleton.CurrentState == State.Lose)) {
            tutorialEndObject = new GameObject();
            tutorialEndObject.AddComponent<TutorialGUI>();
            tutorialEndObject.GetComponent<TutorialGUI>().tutorialText = Resources.Load("tutorial_finished") as TextAsset;
            tutorialEndObject.GetComponent<TutorialGUI>().font = font;
            tutorialEndObject.GetComponent<TutorialGUI>().fontSize = fontSize;
            tutorialEndObject.GetComponent<TutorialGUI>().fontColor = fontColor;
            tutorialEndObject.GetComponent<TutorialGUI>().width = (int)windowDimensions.x;
            tutorialEndObject.GetComponent<TutorialGUI>().height = (int)windowDimensions.y;
        }

        if (tutorialEndObject != null && tutorialEndObject.GetComponent<TutorialGUI>().IsTutorialFinished()) {
            GameObject.Destroy(tutorialEndObject);
            tutorialEndObject = null;
            Application.LoadLevel(nextSceneName);
        }
	}

    private void TutorialSetup() {
        tutorialStartObject = new GameObject();
        tutorialStartObject.AddComponent<TutorialGUI>();
        switch (GameState.Singleton.ScoringMode) {
            case (ScoringMode.Collaborative) :
                tutorialStartObject.GetComponent<TutorialGUI>().tutorialText = Resources.Load("tutorial_collab") as TextAsset;
                break;
            case (ScoringMode.Competitive) :
                tutorialStartObject.GetComponent<TutorialGUI>().tutorialText = Resources.Load("tutorial_compete") as TextAsset;
                break;
            case (ScoringMode.Both) :
                tutorialStartObject.GetComponent<TutorialGUI>().tutorialText = Resources.Load("tutorial_both") as TextAsset;
                break;
            default:
                break; // WE SHOULD NEVER GET HERE
        }
        tutorialStartObject.GetComponent<TutorialGUI>().font = font;
        tutorialStartObject.GetComponent<TutorialGUI>().fontSize = fontSize;
        tutorialStartObject.GetComponent<TutorialGUI>().fontColor = fontColor;
        tutorialStartObject.GetComponent<TutorialGUI>().width = (int)windowDimensions.x;
        tutorialStartObject.GetComponent<TutorialGUI>().height = (int)windowDimensions.y;
        tutorialSetup = true;
    }
}
