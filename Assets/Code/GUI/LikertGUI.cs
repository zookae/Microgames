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
            GUILayout.BeginArea(new Rect((Screen.width - ComputeWidth()) / 2, 0, ComputeWidth(), Screen.height), boxStyle);
            scrollPosition = GUILayout.BeginScrollView(scrollPosition);

            // Header
            textStyle.alignment = TextAnchor.MiddleCenter;
            GUILayout.Label(headerText, textStyle);

            // Populates the questions
            textStyle.alignment = TextAnchor.MiddleLeft;
            foreach (LikertQuestion lq in likertQuestions) {                
                // TODO (add style)
                GUILayout.Label(lq.Question, textStyle);
                if (lq.CanSelectMultiple) {
                    GUILayout.BeginHorizontal();
                    for (int i = 0; i < lq.GetNumOptions(); i++) {
                        bool toggled = GUILayout.Toggle(lq.IsSelected(i), lq.Options[i], toggleStyle);
                        lq.SetOption(i, toggled);
                    }
                    GUILayout.EndHorizontal();
                } else {
                    // We use a selection grid.
                    int currentlySelected = lq.GetSelected();
                    int index = GUILayout.SelectionGrid(currentlySelected, lq.Options, lq.GetNumOptions(), toggleStyle);
                    if (currentlySelected != index) {
                        lq.ToggleOption(index);
                    }
                }
                GUILayout.Space(10);
            }

            GUILayout.EndScrollView();

            // Draws the submission button.
            if (GUILayout.Button("Submit")) {
                if (VerifyResults()) {
                    results = BuildResultString();
                    drawGUI = false;
                } else {
                    // POPUP SOME WEIRD GUI
                    // XXX (kasiu): Technically, we should never get here, since Verify is always valid due to our setup.
                }
            }
            GUILayout.EndArea();
        }

    }

    private bool VerifyResults() {
        foreach (LikertQuestion lq in likertQuestions) {
            if (!lq.ContainsAtLeastOneAnswer() && !lq.CanSelectMultiple) {
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
        return result;
    }

    // HACK (kasiu): Window is 3/5 of the screen. This really should be specified to the component.
    private int ComputeWidth() {
        return (int)((3 * Screen.width) / 5.0f);
    }
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
            bool multipleAnswers = (components[2].Trim().ToLower().Equals("yes")) ? true : false;
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