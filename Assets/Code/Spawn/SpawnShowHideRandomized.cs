using UnityEngine;
using System.Collections;

public class SpawnShowHideRandomized : MonoBehaviour {

    private float elapsed = 0.0f;

    public float showDelay;
    public float delayJitter;
    private float initDelay;

    public float showDuration;
    private float initDuration;
    public float durationJitter;

    private bool isShown;

    public GameObject showObject;

    void Start() {
        isShown = showObject.activeSelf;
        initDelay = showDelay;
        initDuration = showDuration;
    }
	
	// Update is called once per frame
	void Update () {
        elapsed += Time.deltaTime;

        // turn object on after delay before appearing
        if (!isShown && elapsed > showDelay) {
            showObject.SetActive(true);
            isShown = true;
            elapsed = 0.0f;

            showDelay += Random.Range(-delayJitter, delayJitter);
            if (showDelay < 0) {
                showDelay = initDelay;
            }
        } // turn off once duration passed (note: this allows looping)
        else if (isShown && elapsed > showDuration) {
            showObject.SetActive(false);
            isShown = false;
            elapsed = 0.0f;

            showDuration += Random.Range(-durationJitter, durationJitter);
            if (showDuration < 0) {
                showDuration = initDuration;
            }
        }
	}
}
