using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LikertGUI : MonoBehaviour {

    /// <summary>
    /// A text file holding the questions. Eventually change to XML or something.
    /// </summary>
    public TextAsset file;
    public TextAsset headerFile;

    // Font specifics
    public Font font;
    public int fontSize;
    public Color fontColor;
    public Color fontBackground;
    public string closeText;

    private List<LikertQuestion> likertQuestions;
    private bool drawGUI;
    private Vector2 scrollPosition;
    private GUIStyle textStyle;
    private Font oldButtonFont;
    private string headerText;

    // Use this for initialization
	void Start () {
        likertQuestions = LikertParser.ParseLikertFile(file.text);
        headerText = headerFile.text;

        // XXX (kasiu): CHECKING IS STILL NOT ROBUST :P
        if (font != null) {
            textStyle = new GUIStyle();
            textStyle.font = font;
            textStyle.fontSize = fontSize;
            textStyle.normal.textColor = fontColor;
            //textStyle.alignment = TextAnchor.MiddleCenter;
            textStyle.wordWrap = true;
            textStyle.border = new RectOffset(20, 20, 20, 20);
            textStyle.normal.background = GUIUtils.MakeBlankTexture((int)(Screen.width / 2.0f), (int)(Screen.height / 2.0f), fontBackground);
        }

        // Launches draw on start
        drawGUI = true;
	}

    void OnGUI() {
        if (drawGUI) {
            // Don't know if this is necessary, but just resets the font in case.
            if (oldButtonFont == null) {
                oldButtonFont = GUI.skin.button.font;
                GUI.skin.button.font = font;
            }

            // Draws the LIKERT STUFFIE
            GUILayout.BeginArea(new Rect(Screen.width / 4.0f, 0, Screen.width / 2.0f, Screen.height), GUI.skin.box);
            scrollPosition = GUILayout.BeginScrollView(scrollPosition);

            // Header
            GUILayout.Label(headerText);

            // Populates the questions
            foreach (LikertQuestion lq in likertQuestions) {                
                // TODO (add style)
                GUILayout.Label(lq.Question, textStyle);
                if (lq.CanSelectMultiple) {
                    GUILayout.BeginHorizontal();
                    for (int i = 0; i < lq.GetNumOptions(); i++) {
                        bool toggled = GUILayout.Toggle(lq.IsSelected(i), lq.Options[i]);
                        lq.SetOption(i, toggled);
                    }
                    GUILayout.EndHorizontal();
                } else {
                    // We use a selection grid.
                    int currentlySelected = lq.GetSelected();
                    int index = GUILayout.SelectionGrid(currentlySelected, lq.Options, lq.GetNumOptions(), "toggle");
                    if (currentlySelected != index) {
                        lq.ToggleOption(index);
                    }
                }
            }

            GUILayout.EndScrollView();

            // Draws the submission button.
            if (GUILayout.Button("Submit")) {
                if (VerifyResults()) {
                    drawGUI = false;
                    // SUBMIT THINGS
                    DebugConsole.LogError(GetResultString());
                } else {
                }
            }
            GUILayout.EndArea();
        }

    }

    private bool VerifyResults() {
        foreach (LikertQuestion lq in likertQuestions) {
            if (!lq.ContainsAtLeastOneAnswer()) {
                return false;
            }
        }
        return true;
    }

    private string GetResultString() {
        string result = "";
        for (int i = 0; i < likertQuestions.Count; i++) {
            LikertQuestion lq = likertQuestions[i];
            result += lq.AnswerToString();
            if (i != likertQuestions.Count - 1) {
                result += ":";
            }
        }
        return result;
    }
}

public static class LikertParser
{
    public static List<LikertQuestion> ParseLikertFile(string text) {
        List<LikertQuestion> list = new List<LikertQuestion>();
        // Assumes each questions is on a newline.
        string[] questions = text.Split('\n');

        foreach (string question in questions) {
            // Question components are split with colons.
            string[] components = question.Split(':');
            if (components.Length != 3) {
                DebugConsole.LogError("Poorly formatted likert file. Aborting with empty list. " + components.Length);
                return new List<LikertQuestion>();
            }
            // Choices are split with commas.
            string[] options = components[1].Split(',');
            bool multipleAnswers = (components[2].ToLower().Equals("yes")) ? true : false;
            LikertQuestion lq = new LikertQuestion(components[0], options, multipleAnswers);
            list.Add(lq);
        }

        return list;
    }
}


public class LikertQuestion 
{
    public string Question { get; private set;}
    public string[] Options { get; private set;}
    public bool CanSelectMultiple { get; private set; }

    private bool[] optionsSelected;

    public LikertQuestion(string question, string[] options, bool canSelectMultiple) {
        this.Question = question;
        this.Options = options;
        this.CanSelectMultiple = canSelectMultiple;

        // Bools all default to false
        optionsSelected = new bool[Options.Length];

        // Need to set a default value for the can-select multiple
        // Defaults to the middle value
        if (!CanSelectMultiple) {
            int index = Options.Length / 2;
            if (Options.Length % 2 != 0) {
                index = (Options.Length + 1) / 2;
            }
            optionsSelected[index] = true;
        }
    }

    public int GetNumOptions() {
        return Options.Length;
    }

    public void SetOption(int index, bool value) {
        if (index < 0 || index > Options.Length) {
            return;
        }
        optionsSelected[index] = value;
    }

    public bool ToggleOption(int index) {
        if (index < 0 || index >= Options.Length) {
            return false;
        }

        if (CanSelectMultiple) {
            optionsSelected[index] = !optionsSelected[index];
            return true;
        } else {
            // If we're flipping from false to true, any other true elements must be turned off.
            if (!optionsSelected[index]) {
                for (int i = 0; i < Options.Length; i++) {                    
                    if (optionsSelected[i]) {
                        optionsSelected[i] = !optionsSelected[i];
                    }
                }
            }
            optionsSelected[index] = !optionsSelected[index];
            return true;
        }
    }

    public bool IsSelected(int index) {
        if (index < 0 || index >= Options.Length) {
            return false;
        }
        return optionsSelected[index];
    }

    // If multiple answers can be selected, this only returns the first one.
    public int GetSelected() {
        for (int i = 0; i < Options.Length; i++) {
            if (optionsSelected[i]) {
                return i;
            }
        }
        return -1;
    }

    public bool ContainsAtLeastOneAnswer() {
        foreach (bool b in optionsSelected) {
            if (b) {
                return true;
            }
        }
        return false;
    }

    public string AnswerToString() {
        string answer = "";
        if (CanSelectMultiple) {
            answer += "(";
        }
        for (int i = 0; i < Options.Length; i++) {
            if (optionsSelected[i]) {
                answer += Options[i];
                if (CanSelectMultiple) {
                    answer += ";";
                }
            }
        }
        if (CanSelectMultiple) {
            answer += ")";
        }
        return answer;
    }

    // HAH. NO DEFAULT CONSTRUCTOR FOR YOUUU
    private LikertQuestion() : this(null, null, false) { }
}