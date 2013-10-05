using UnityEngine;
using System.Collections;

public class ParameterSlider : MonoBehaviour {

    public float xPos;
    public float yPos;
    public float xSize;
    public float ySize;

    public int fontSize;

    public float paramMin;
    public float paramMax;
    public string paramName;

    public float LabelSlider(Rect screenRect, float paramVal, int fontSize) {
        GUIStyle style = new GUIStyle();
        style.fontSize = fontSize;

        Rect labelRect = screenRect;
        labelRect.x += screenRect.width;
        GUI.Label(labelRect, paramName, style);

        // &lt;- Push the Slider to the end of the Label
        //screenRect.x += screenRect.width;

        paramVal = GUI.HorizontalSlider(screenRect, paramVal, paramMin, paramMax);
        return paramVal;
    }
}
