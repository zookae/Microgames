using UnityEngine;
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

	// Use this for initialization
	void Start () {
        width = (3 * Screen.width) / 5;
        height = Screen.height / 5;
        surveySent = false;

        // XXX (kasiu): CHECKING IS STILL NOT ROBUST :P
        if (font != null) {
            textStyle = new GUIStyle();
            textStyle.font = font;
            textStyle.fontSize = fontSize;
            textStyle.normal.textColor = fontColor;
            //textStyle.alignment = TextAnchor.MiddleCenter;
            textStyle.wordWrap = true;
            textStyle.margin = new RectOffset(0, 0, 0, 0);
            textStyle.normal.background = GUIUtils.MakeBlankTexture(width, height, fontBackground);
        }
	}

    public bool IsSurveyRound() {
        return (GameRoundCounter.GetCurrentRound() == surveyRound);
    }

    private string GenerateScoreText() {
        string text = "";
        text += "Your score for this round was " + (int)GameState.Singleton.score + " points!\n";
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
                    Application.LoadLevel(gameSceneName);
                }
                GUILayout.EndArea();
            } else if (GameRoundCounter.GetCurrentRound() == surveyRound) {
                // Spawn the survey object
                switch (surveyRoundState) {
                    case SurveyRoundState.WaitingToSpawn:
                        surveyObject = new GameObject();
                        surveyObject.transform.position = new Vector3(0, 0, -5f);
                        surveyObject.AddComponent<LikertGUI>();
                        LikertGUI lc = surveyObject.GetComponent<LikertGUI>();
                        SetupLikertGUI(ref lc);
                        surveyRoundState = SurveyRoundState.SurveyObjectSpawned;
                        break;
                    case SurveyRoundState.SurveyObjectSpawned:
                        // Draw nothing
                        break;
                    case SurveyRoundState.SurveyResultsSent:
                        // Display continue dialogue
                        string text = "That's it! Thanks for playing!" + '\n' + "The study is over, but you can play some more rounds or return to the start menu if you like. If you choose to play more rounds, you can quit at any time.";
                        // Otherwise, spawn a box that lets the player quit or continue
                        DisplayContinueOrQuitDialogue(text);
                        break;
                }
            } else {
                // Display dialogue
                // Otherwise, spawn a box that lets the player quit or continue
                DisplayContinueOrQuitDialogue(scoreText);
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
            Application.LoadLevel(gameSceneName);
        }
        if (GUILayout.Button("Quit to Start!", buttonStyle)) {
            drawGUI = false;
            GameRoundCounter.AdvanceRound();
            Application.LoadLevel(titleSceneName);
        }
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }
	
	// Update is called once per frame
	void Update () {
        // Listens in on GameState for the number of rounds
        if ((GameState.Singleton.CurrentState == State.Win ||
            GameState.Singleton.CurrentState == State.Lose) &&
            !drawGUI) {
                drawGUI = true;
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