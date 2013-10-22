using UnityEngine;
using System.Collections;

public class RestartEntityCount : MonoBehaviour {

    /// <summary>
    /// Tag of entity that must be present at count amount
    /// </summary>
    public string countTag;

    /// <summary>
    /// Amount of quantity to trigger restart
    /// </summary>
    public int countAmount;

    private GameObject[] targetSet;

    private float checkFreq = 1;
    private float timeDelta = 0.0f;

	void Awake () {
        targetSet = GameObject.FindGameObjectsWithTag(countTag);
	}
	
	void Update () {
        timeDelta += Time.deltaTime;

        // check periodically
        if (timeDelta > checkFreq) {
            timeDelta = 0.0f;

            targetSet = GameObject.FindGameObjectsWithTag(countTag); // update set of objects
            if (targetSet.Length == countAmount) {
                Application.LoadLevel(Application.loadedLevel); // restart level
            }
        }
	}
}
