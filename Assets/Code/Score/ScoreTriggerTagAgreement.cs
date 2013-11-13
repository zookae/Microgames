using UnityEngine;
using System.Collections;

public class ScoreTriggerTagAgreement : MonoBehaviour {

    /// <summary>
    /// The base score for labeling (and presumably matching).
    /// </summary>
    public float baseScore = 0;

    /// <summary>
    /// The bonus score for agreeing
    /// </summary>
    public float agreementBonus = 10;

    // TODO (kasiu): Think about whether it's necessary to keep all of this state around.
    //               It's not, but it saves iterating over the game state stuff on every update.
    /// <summary>
    /// Whether or not this component has been clicked (tag selected)
    /// </summary>
    public bool wasTagged { get; private set; }

    /// <summary>
    /// Whether or not agreement has occured (secondary)
    /// </summary>
    public bool wasAgreedOn { get; private set; }

    /// <summary>
    /// The assigned tag
    /// </summary>
    public string tag { get; private set; }

	// Use this for initialization
	void Start () {
        wasTagged = false;
        wasAgreedOn = false;
        tag = null;
	}
	
	// Update is called once per frame
	void Update () {
        if ((wasTagged && wasAgreedOn) || (GameState.Singleton.CurrentState != State.Running)) {
            return;
        }
        // Check for if the object has been clicked on.
        // XXX (kasiu): Assigns points for the tag select, but does not check if it matches gold standard.
        if (!wasTagged) {
            foreach (Triple<double, string, string> click in GameState.Singleton.clickTrace) {
                if (click.Second == transform.name) {
                    wasTagged = true;
                    tag = click.Third;
                    GameState.Singleton.score += baseScore;
                    Debug.Log("OH GOODNESS! OUR SCORE CHANGED!");
                    break;
                }
            }
        }
        // Check if the partner has agreed (may be delayed).
        if (wasTagged && !wasAgreedOn) {
            foreach (Triple<double, string, string> click in GameState.Singleton.partnerTrace) {
                if (click.Second == transform.name && click.Third == tag) {
                    wasAgreedOn = true;
                    GameState.Singleton.score += agreementBonus;
                    Debug.Log("BONUS CHEESECAKE! OUR PARTNER AGREED!");
                    break;
                }
            }
        }
	}
}