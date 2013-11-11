using UnityEngine;
using System.Collections;

public class TutorialGUI : MonoBehaviour {

    public TextAsset tutorialText;

    private Vector2[] positions;
    private string[] instructions;
    private int numInstructions;

    // Font specifics
    public Font font;
    public int fontSize;
    public Color fontColor;
    public Color fontBackground;
    public string closeText;

    // Hidden
    private int currentInstruction;
    private bool drawGUI = false;
    private bool tutorialViewed = false;
    private Vector2 scrollPosition;
    private GUIStyle textStyle;
    private GUIStyle buttonStyle;

	// Use this for initialization
	void Start () {       
        drawGUI = false;
        currentInstruction = -1;
        numInstructions = -1;

        // XXX (kasiu): Checking is not robust
        if (font != null) {
            textStyle = new GUIStyle();
            textStyle.font = font;
            textStyle.fontSize = fontSize;
            textStyle.normal.textColor = fontColor;
            textStyle.wordWrap = true;
            textStyle.border = new RectOffset(20, 20, 20, 20);
            textStyle.normal.background = GUIUtils.MakeBlankTexture((int)(Screen.width / 2.0f), (int)(Screen.height / 2.0f), fontBackground);
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
            GUILayout.BeginArea(new Rect(Screen.width / 4.0f, Screen.height / 4.0f, Screen.width / 2.0f, Screen.height / 2.0f), GUI.skin.box);
            scrollPosition = GUILayout.BeginScrollView(scrollPosition);
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
        } else if (instructions != null && !drawGUI && !tutorialViewed) {
            // Start up!
            currentInstruction = 0;
            numInstructions = instructions.Length;
            drawGUI = true;
        }
	
	}
}
