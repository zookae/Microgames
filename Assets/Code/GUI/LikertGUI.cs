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
    private string headerText;

    // HAH! Figured out the style stuff!
    private GUIStyle textStyle;
    private GUIStyle toggleStyle;
    private GUIStyle boxStyle;

    private bool printErrorText;

    // Caches the results and returns them
    private string results;

    /// <summary>
    /// Will only return results once the actual survey is finished.
    /// Other components should call this function to listen for results.
    /// </summary>
    /// <returns>The results as a formatted string, null if the survey has not been completed</returns>
    public string GetResults() {
        return results;
    }

    // Use this for initialization
	void Start () {
        // HACK (kasiu): Using '^'-delimeted strings. It's ugly. I'm lazy. Deal with it.
        likertQuestions = LikertParser.ParseLikertFile(file.text, '^');
        headerText = headerFile.text;

        // XXX (kasiu): CHECKING IS STILL NOT ROBUST :P
        if (font != null) {
            textStyle = new GUIStyle();
            textStyle.font = font;
            textStyle.fontSize = fontSize;
            textStyle.normal.textColor = fontColor;
            //textStyle.alignment = TextAnchor.MiddleCenter;
            textStyle.wordWrap = true;
            textStyle.margin = new RectOffset(0, 0, 0, 0);
        }

        // Launches draw on start
        results = null;
        drawGUI = true;
        printErrorText = false;
	}

    void OnGUI() {
        if (drawGUI) {
            if (boxStyle == null && toggleStyle == null) {
                boxStyle = new GUIStyle(GUI.skin.box);
                boxStyle.normal.background = GUIUtils.MakeBlankTexture(ComputeWidth(), (int)(Screen.height / 2.0f), fontBackground);
                toggleStyle = new GUIStyle(GUI.skin.toggle);
                toggleStyle.font = font;
                toggleStyle.normal.textColor = fontColor;
                toggleStyle.onNormal.textColor = fontColor;
                toggleStyle.onHover.textColor = Color.gray;
                toggleStyle.fontSize = fontSize;
            }

            // Draws the LIKERT STUFFIE
            GUI.depth = (int)GUIDepthLevels.INFO_WINDOW;
            GUILayout.BeginArea(new Rect((Screen.width - ComputeWidth()) / 2, 0, ComputeWidth(), Screen.height), GUI.skin.box);
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, boxStyle);

            // Header
            textStyle.alignment = TextAnchor.MiddleCenter;
            GUILayout.Label(headerText, textStyle);

            if (printErrorText) {
                Color fontColor = textStyle.normal.textColor;
                textStyle.normal.textColor = Color.red;
                GUILayout.Label("Looks like there was an error filling out the survey.", textStyle);
                textStyle.normal.textColor = fontColor;
            }

            // Populates the questions
            textStyle.alignment = TextAnchor.MiddleLeft;
            foreach (LikertQuestion lq in likertQuestions) {                
                // TODO (add style)
                GUILayout.Label(lq.Question, textStyle);
                if (lq.QuestionType == SurveyQuestionType.MultipleOptionSelect) {
                    GUILayout.BeginHorizontal();
                    for (int i = 0; i < lq.GetNumOptions() / 2; i++) {
                        bool toggled = GUILayout.Toggle(lq.IsSelected(i), lq.Options[i], toggleStyle);
                        lq.SetOption(i, toggled);
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    for (int i = lq.GetNumOptions() / 2; i < lq.GetNumOptions(); i++) {
                        bool toggled = GUILayout.Toggle(lq.IsSelected(i), lq.Options[i], toggleStyle);
                        lq.SetOption(i, toggled);
                    }
                    GUILayout.EndHorizontal();
                } else if (lq.QuestionType == SurveyQuestionType.SingleOptionSelect) {
                    // We use a selection grid.
                    int currentlySelected = lq.GetSelected();
                    int index = GUILayout.SelectionGrid(currentlySelected, lq.Options, lq.GetNumOptions(), toggleStyle);
                    if (currentlySelected != index) {
                        lq.ToggleOption(index);
                    }
                } else if (lq.QuestionType == SurveyQuestionType.ManualEntry) {
                    string manualEntry = (lq.GetManualText() == null) ? "" : lq.GetManualText();
                    string newEntry = GUILayout.TextField(manualEntry, 3);
                    lq.SetManualText(newEntry);
                }
                GUILayout.Space(10);
            }

            GUILayout.EndScrollView();

            // Draws the submission button.
            if (GUILayout.Button("Submit")) {
                if (VerifyResults()) {
                    results = BuildResultString();
                    drawGUI = false;
                    printErrorText = false;
                } else {
                    // POPUP SOME WEIRD GUI
                    // XXX (kasiu): Technically, we should never get here, since Verify is always valid due to our setup.
                    printErrorText = true;
                }
            }
            GUILayout.EndArea();
        }

    }

    private bool VerifyResults() {
        foreach (LikertQuestion lq in likertQuestions) {
            if (!lq.ContainsAtLeastOneAnswer() && lq.QuestionType == SurveyQuestionType.SingleOptionSelect) {
                return false;
            }
            if (lq.QuestionType == SurveyQuestionType.ManualEntry) {
                string answer = lq.GetManualText();
                if (answer == null || answer.Length < 2 || !ContainsOnlyNumbers(answer)) {
                    return false;
                }
            }
        }
        return true;
    }

    private bool ContainsOnlyNumbers(string answer) {
        string trimmedAnswer = answer.Trim();
        foreach (char c in trimmedAnswer) {
            if (!char.IsDigit(c)) {
                Debug.Log(answer + " is contains " + c + " which is not a digit ");
                return false;
            }
        }
        return true;
    }

    private string BuildResultString() {
        string result = "";
        for (int i = 0; i < likertQuestions.Count; i++) {
            LikertQuestion lq = likertQuestions[i];
            result += lq.AnswerToString();
            if (i != likertQuestions.Count - 1) {
                result += ":";
            }
        }
        Debug.Log(result);
        return result;
    }

    // HACK (kasiu): Window is 3/5 of the screen. This really should be specified to the component.
    private int ComputeWidth() {
        return (int)((3 * Screen.width) / 5.0f);
    }
}

public enum SurveyQuestionType
{
    SingleOptionSelect,
    MultipleOptionSelect,
    ManualEntry,
    Invalid
}

public static class LikertParser
{
    public static List<LikertQuestion> ParseLikertFile(string text, char delimeter) {
        List<LikertQuestion> list = new List<LikertQuestion>();
        // Assumes each questions is on a newline.
        string[] questions = text.Split('\n');

        foreach (string question in questions) {
            // Question components are split with colons.
            string[] components = question.Split(delimeter);
            if (components.Length != 3) {
                DebugConsole.LogError("Poorly formatted likert file. Aborting with empty list. " + components.Length);
                return new List<LikertQuestion>();
            }
            // Choices are split with commas.
            string[] options = components[1].Split(',');
            SurveyQuestionType questionType;
            string type = components[2].Trim().ToLower();
            switch (type) {
                case "yes":
                    questionType = SurveyQuestionType.MultipleOptionSelect;
                    break;
                case "no":
                    questionType = SurveyQuestionType.SingleOptionSelect;
                    break;
                case "text":
                    questionType = SurveyQuestionType.ManualEntry;
                    break;
                default:
                    questionType = SurveyQuestionType.Invalid;
                    break;
            }
            LikertQuestion lq = new LikertQuestion(components[0], options, questionType);
            list.Add(lq);
        }

        return list;
    }
}


public class LikertQuestion 
{
    public string Question { get; private set;}
    public string[] Options { get; private set;}
    public SurveyQuestionType QuestionType { get; private set; }

    private bool[] optionsSelected;
    private string manualText;

    public LikertQuestion(string question, string[] options, SurveyQuestionType questionType) {
        this.Question = question;
        this.Options = options;
        this.QuestionType = questionType;

        // Bools all default to false
        optionsSelected = new bool[Options.Length];
        manualText = "";

        // Need to set a default value for the single-selects
        // Defaults to the middle value
        if (QuestionType == SurveyQuestionType.SingleOptionSelect) {
            int index = Options.Length / 2;
            if (Options.Length % 2 != 0) {
                index = (Options.Length + 1) / 2;
            }
            index = (index <= 0) ? 0 : index - 1;
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

        if (QuestionType == SurveyQuestionType.MultipleOptionSelect) {
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

    public string GetManualText() {
        if (QuestionType != SurveyQuestionType.ManualEntry) {
            return null;
        }
        return manualText;
    }

    public bool SetManualText(string text) {
        if (QuestionType != SurveyQuestionType.ManualEntry || text == null) {
            return false;
        }
        manualText = text;
        return true;
    }

    public string AnswerToString() {
        string answer = "";

        if (QuestionType == SurveyQuestionType.ManualEntry) {
            return (manualText == null) ? answer : manualText;
        }

        if (QuestionType == SurveyQuestionType.MultipleOptionSelect) {
            answer += "(";
        }
        for (int i = 0; i < Options.Length; i++) {
            if (optionsSelected[i]) {
                answer += Options[i];
                if (QuestionType == SurveyQuestionType.MultipleOptionSelect) {
                    answer += ",";
                }
            }
        }
        if (QuestionType == SurveyQuestionType.MultipleOptionSelect) {
            // This 
            if (answer[answer.Length - 1] == ',') {
                // Removes trailing comma
                answer = answer.Substring(0, answer.Length - 1);
            }
            answer += ")";
        }
        return answer;
    }

    // HAH. NO DEFAULT CONSTRUCTOR FOR YOUUU
    private LikertQuestion() : this(null, null, SurveyQuestionType.Invalid) { }
}