using UnityEngine;
using System.Collections;

public class TutorialGUI : MonoBehaviour {
    /// <summary>
    /// Assumes the text file can be split on newlines.
    /// Each line is a separate instruction.
    /// </summary>
    public TextAsset tutorialText;
    public int width;
    public int height;

    // Font specifics
    public Font font;
    public int fontSize;
    public Color fontColor;

    // Hidden
    private Vector2[] positions;
    private string[] instructions;
    private int currentInstruction;
    private bool drawGUI = false;
    private bool tutorialViewed = false;
    private Vector2 scrollPosition;
    private GUIStyle textStyle;
    private GUIStyle buttonStyle;
    private GUIStyle boxStyle;

    public bool IsTutorialFinished() {
        return tutorialViewed;
    }

	// Use this for initialization
	void Start () {
        if (tutorialText != null) {
            ParseTextAsset();
        } else {
            drawGUI = false;
            currentInstruction = -1;
        }

        width = (width <= 0) ? 100 : width;
        height = (height <= 0) ? 100 : height;

        // XXX (kasiu): Checking is not robust
        if (font != null) {
            textStyle = new GUIStyle();
            textStyle.font = font;
            textStyle.fontSize = fontSize;
            textStyle.normal.textColor = fontColor;
            textStyle.wordWrap = true;
            textStyle.border = new RectOffset(20, 20, 20, 20);
        }
	}

    private void DecrementInstruction() {
        currentInstruction = (currentInstruction == 0) ? currentInstruction : currentInstruction - 1;
    }

    private void IncrementInstruction() {
        currentInstruction = (currentInstruction == instructions.Length - 1) ? currentInstruction : currentInstruction + 1;
    }

    void OnGUI() {
        if (drawGUI) {
            if (buttonStyle == null) {
                buttonStyle = new GUIStyle(GUI.skin.button);
                buttonStyle.font = font;
                boxStyle = new GUIStyle(GUI.skin.box);
                boxStyle.normal.background = GUIUtils.MakeBlankTexture((int)(Screen.width / 2.0f), (int)(Screen.height / 2.0f), new Color(1.0f, 1.0f, 1.0f, 0.8f));
            }
            
            Vector2 pos = GUIUtils.ComputeCenteredPosition(width, height);
            GUILayout.BeginArea(new Rect(pos.x, pos.y, width, height), GUI.skin.box);
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, boxStyle);
            GUILayout.Label(instructions[currentInstruction], textStyle);
            GUILayout.EndScrollView();
            GUILayout.BeginHorizontal();
            if (currentInstruction > 0) {
                if (GUILayout.Button("Back", buttonStyle)) {
                    DecrementInstruction();
                }
            }
            string buttonText = (currentInstruction == instructions.Length - 1) ? "Finish" : "Next";
            if (GUILayout.Button(buttonText, buttonStyle)) {
                if (currentInstruction == instructions.Length - 1) {
                    tutorialViewed = true;
                    drawGUI = false;
                } else {
                    IncrementInstruction();
                }
            }
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (instructions == null && !drawGUI) {
            drawGUI = false;
        } else if (tutorialText != null && !drawGUI && !tutorialViewed) {
            // Start up!
            ParseTextAsset();
        }	
	}

    public void ParseTextAsset() {
        instructions = tutorialText.text.Split('\n');
        currentInstruction = 0;
        drawGUI = true;
    }
}
