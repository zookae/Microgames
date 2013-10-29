using UnityEngine;
using System.Collections;

public class TextEmphasis : MonoBehaviour {

    // TODO (kasiu): Eventually use these.
    //public Color normalColor = Color.black;
    //public Color emphasisColor = Color.black;

    void Start() {
        // TODO (kasiu): Set font colors.
    }

    void OnMouseDown() {
        GUIText text = this.gameObject.GetComponent<GUIText>();        
        if (text != null) {
            text.fontStyle = FontStyle.Bold;
        }
    }

    // Used to handle the case where the user drags the mouse over a label and releases outside of the collider.
    void OnMouseExit() {
        GUIText text = this.gameObject.GetComponent<GUIText>();
        if (text != null && text.fontStyle == FontStyle.Bold) {
            text.fontStyle = FontStyle.Normal;
        }
    }

    void OnMouseUpAsButton() {
        GUIText text = this.gameObject.GetComponent<GUIText>();
        if (text != null) {
            text.fontStyle = FontStyle.Normal;
        }
    }
}
