﻿using UnityEngine;
using System.Collections;

public enum SurveyRoundState
{
    WaitingToSpawn,
    SurveyObjectSpawned,
    SurveyResultsSent
}

/// <summary>
/// HACK (kasiu): This thing is a wreck. I'll deal with it later.
/// </summary>
public class EndGameDialogueManager : MonoBehaviour {

    // Keeps track of important levels
    // Assumes only one survey round
    public int surveyRound;

    // Keeps track of important scenes
    public string titleSceneName;
    public string gameSceneName;

    private bool drawGUI = false;
    private GameObject surveyObject = null;
    private SurveyRoundState surveyRoundState = SurveyRoundState.WaitingToSpawn;

    // Font specifics
    public Font font;
    public int fontSize;
    public Color fontColor;
    public Color fontBackground;
    public string closeText;

    // Score box dimensions
    private int width;
    private int height;
    private Vector2 scrollPosition;

    private GUIStyle textStyle;
    private GUIStyle buttonStyle;
    private GUIStyle boxStyle;

    private bool surveySent;
	private int totalScore;

	// Use this for initialization
	void Start () {
        width = (3 * Screen.width) / 5;
        height = Screen.height / 5;
        surveySent = false;
		totalScore = 0;

        // XXX (kasiu): CHECKING IS STILL NOT ROBUST :P
        if (font != null) {
            textStyle = new GUIStyle();
            textStyle.font = font;
            textStyle.fontSize = fontSize;
            textStyle.normal.textColor = fontColor;
            //textStyle.alignment = TextAnchor.MiddleCenter;
            textStyle.wordWrap = true;
            textStyle.margin = new RectOffset(0, 0, 0, 0);
            //textStyle.normal.background = GUIUtils.MakeBlankTexture(width, height, fontBackground);
        }
	}

    public bool IsSurveyRound() {
        return (GameRoundCounter.GetCurrentRound() == surveyRound);
    }

    private string GenerateScoreText() {
        string text = "";
        text += "Your score for this round was " + (int)GameState.Singleton.score + " points!\n";
		text += "Your total score is now " + totalScore + " points!\n";
        text += "You assigned " + GameState.Singleton.clickTrace.Count + (GameState.Singleton.clickTrace.Count == 1 ? " object" : " objects") + "!\n";
        text += "Your partner assigned " + GameState.Singleton.partnerTrace.Count + (GameState.Singleton.partnerTrace.Count == 1 ? " object" : " objects") + "!\n";
        text += "Great job!";
        return text;
    }

    private void SetupLikertGUI(ref LikertGUI lg) {
        lg.file = Resources.Load("likert") as TextAsset;
        lg.headerFile = Resources.Load("likert_header") as TextAsset;
        lg.font = font;
        lg.fontSize = fontSize;
        lg.fontColor = fontColor;
        lg.fontBackground = fontBackground;
        lg.nextSceneName = titleSceneName;
    }

    void OnGUI() {
        if (drawGUI) {
            if (boxStyle == null) {
                boxStyle = new GUIStyle(GUI.skin.box);
                boxStyle.normal.background = GUIUtils.MakeBlankTexture(width, height, fontBackground);
                buttonStyle = new GUIStyle(GUI.skin.button);
                buttonStyle.font = font;
                buttonStyle.fontSize = fontSize;
            }

            string scoreText = GenerateScoreText();

            if (GameRoundCounter.GetCurrentRound() < surveyRound) {
                // Draw continue dialogue
                GUILayout.BeginArea(new Rect((Screen.width - width) / 2.0f, (Screen.height - height) / 2.0f, width, height), GUI.skin.box);

                scrollPosition = GUILayout.BeginScrollView(scrollPosition, boxStyle);
                GUILayout.Label(scoreText, textStyle);
                GUILayout.EndScrollView();

                // BUTTONS
                if (GUILayout.Button("Continue!", buttonStyle)) {
                    drawGUI = false;
                    GameRoundCounter.AdvanceRound();
					GameRoundCounter.AddScore((int)GameState.Singleton.score);					
                    // The following line is a hack to keep the survey from drawing after the round changes.
                    surveyRoundState = SurveyRoundState.SurveyObjectSpawned;                    
                    Application.LoadLevel(gameSceneName);
                }
                GUILayout.EndArea();
            } else {
                // Spawn the survey object
                switch (surveyRoundState) {
                    case SurveyRoundState.WaitingToSpawn:
                        string text = '\n' + "Feel free to continue or finish up with the post-game survey.";
                        // Otherwise, spawn a box that lets the player quit or continue
                        DisplayContinueOrQuitDialogue(scoreText + text);
                        break;
                    case SurveyRoundState.SurveyObjectSpawned:
                    case SurveyRoundState.SurveyResultsSent:
                        // Draw nothing
                        break;
                }
            }
        }
    }

    private void DisplayContinueOrQuitDialogue(string text) {
        // Otherwise, spawn a box that lets the player quit or continue
        GUILayout.BeginArea(new Rect((Screen.width - width) / 2.0f, (Screen.height - height) / 2.0f, width, height), GUI.skin.box);

        scrollPosition = GUILayout.BeginScrollView(scrollPosition, boxStyle);
        GUILayout.Label(text, textStyle);
        GUILayout.EndScrollView();

        // BUTTONS
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Continue!", buttonStyle)) {
            drawGUI = false;
            GameRoundCounter.AdvanceRound();
			GameRoundCounter.AddScore((int)GameState.Singleton.score);			
			Application.LoadLevel(gameSceneName);
        }
        if (GUILayout.Button("Finish!", buttonStyle)) {
            surveyObject = new GameObject();
            surveyObject.transform.position = new Vector3(0, 0, -5f);
            surveyObject.AddComponent<LikertGUI>();
            LikertGUI lc = surveyObject.GetComponent<LikertGUI>();
            SetupLikertGUI(ref lc);
            surveyRoundState = SurveyRoundState.SurveyObjectSpawned;
        }
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }
	
	// Update is called once per frame
	void Update () {
        // Listens in on GameState for the number of rounds
        if ((GameState.Singleton.CurrentState == State.Win ||
            GameState.Singleton.CurrentState == State.Lose) &&
            (!drawGUI && surveyRoundState != SurveyRoundState.SurveyResultsSent)) {
            // That last clause fixes a bug where the score window can pop up again by accident.
                drawGUI = true;
				if (totalScore == 0) {
					totalScore = (int)GameState.Singleton.score + GameRoundCounter.GetTotalScore();
				}
        }

        if (surveyObject != null) {
            LikertGUI lc2 = surveyObject.GetComponent<LikertGUI>();
            string results = lc2.GetResults();
            if (results != null && !surveySent) {
                // SEND THE SURVEY RESULTS OFF....
                this.gameObject.GetComponent<DBGameStateManager>().SendSurveyResults(results);
                surveyRoundState = SurveyRoundState.SurveyResultsSent;
                surveySent = true;
            }
        }
	}
}