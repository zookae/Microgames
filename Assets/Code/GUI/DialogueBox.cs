using UnityEngine;
using System.Collections;

public class DialogueBox : MonoBehaviour {

    public Vector2 position;
    public string text;
    public bool active;
    private bool closed;

	// Use this for initialization
	void Start () {
        closed = false;
        active = false;
	}

    void ToggleRenderable() {
    }

    void OnGUI() {
        if (active && !closed) {
            // Draw
            
        }
    }

    void OnMouseUpAsButton() {
        closed = true;
        active = false;
    }
   
	
	// Update is called once per frame
	void Update () {}
}
