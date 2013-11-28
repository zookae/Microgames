using UnityEngine;
using System.Collections;

public class SpawnShowHide : MonoBehaviour {

    private float elapsed = 0.0f;
    public float showDelay;
    public float showDuration;

    private bool isShown;

    public GameObject showObject;

    void Start() {
        isShown = showObject.activeSelf;
    }
	
	// Update is called once per frame
	void Update () {
        elapsed += Time.deltaTime;

        // turn object on after delay before appearing
        if (!isShown && elapsed > showDelay) {
            showObject.SetActive(true);
            isShown = true;
            elapsed = 0.0f;
        } // turn off once duration passed (note: this allows looping)
        else if (isShown && elapsed > showDuration) {
            showObject.SetActive(false);
            isShown = false;
            elapsed = 0.0f;
        }
	}
}
