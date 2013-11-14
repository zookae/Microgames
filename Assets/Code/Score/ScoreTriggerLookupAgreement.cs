using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScoreTriggerLookupAgreement : MonoBehaviour {

    /// <summary>
    /// The base score for labeling (and presumably matching).
    /// </summary>
    public float baseScore = 10;

    // TODO (kasiu): Think about whether it's necessary to keep all of this state around.
    //               It's not, but it saves iterating over the game state stuff on every update.
    /// <summary>
    /// Whether or not this component has been clicked (tag selected)
    /// </summary>
    public bool wasTagged { get; private set; }

    // Use this for initialization
    void Start() {
        wasTagged = false;
    }

    // Update is called once per frame
    void Update() {
        if (wasTagged || (GameState.Singleton.CurrentState != State.Running)) {
            return;
        }
        // Check for if the object has been clicked on.
        // XXX (kasiu): Assigns points for the tag select, but does not check if it matches gold standard.
        if (!wasTagged) {
            foreach (Triple<double, string, string> click in GameState.Singleton.clickTrace) {
                if (transform.name == click.Second) {
                    string tagName;
                    if (GameState.Singleton.ScoringMode == ScoringMode.Both) {
                        tagName = TagUtils.TrimBothModeTag(click.Third);
                    } else {
                        tagName = click.Third;
                    }

                    if (ScoreStandardDictionary.MatchesStandard(click.Second, tagName)) {
                        wasTagged = true;
                        string str = "+" + baseScore + " points!";
                        Debug.Log(str);
                        // TODO (kasiu): Spawn score thing.
                        GameState.Singleton.score += baseScore;
                        break;
                    }
                }
            }
        }
    }
}