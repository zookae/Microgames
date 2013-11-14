using UnityEngine;
using System.Collections;

public class ScoreTriggerTagBlocked : MonoBehaviour {

    /// <summary>
    /// The base score for labeling (and matching the gold standard).
    /// </summary>
    public float baseScore = 0;

    /// <summary>
    /// The penalty for being blocked.
    /// </summary>
    public float blockPenalty = 10;

    /// <summary>
    /// Whether or not this component has been tagged.
    /// </summary>
    public bool wasTagged { get; private set; }

    /// <summary>
    /// Whether or not the block has been resolved for this player.
    /// </summary>
    public bool wasBlockResolved { get; private set; }

    /// <summary>
    /// The saved click (once we find it) to use for future score resolution.
    /// </summary>
    public Triple<double, string, string> click { get; private set; }

	// Use this for initialization
	void Start () {
        wasTagged = false;
        wasBlockResolved = false;
        click = null;	
	}
	
	// Update is called once per frame
	void Update () {
        if ((wasTagged && wasBlockResolved) || (GameState.Singleton.CurrentState != State.Running)) {
            return;
        }
        // Check for if the object has been clicked on.
        // XXX (kasiu): Assigns points for the tag select, but does not check if it matches gold standard.
        if (!wasTagged) {
            foreach (Triple<double, string, string> ct in GameState.Singleton.clickTrace) {
                if (ct.Second == transform.name) {
                    wasTagged = true;
                    this.click = ct;
                    GameState.Singleton.score += baseScore;
                    //Debug.Log("OH GOODNESS! OUR SCORE CHANGED!");
                    break;
                }
            }

            // Check to see if it's been blocked (and dock points).
            // NOTE (kasiu): This really doesn't need to be isolated: refactor back into above loop
            if (wasTagged) {
                foreach (Triple<double, string, string> pt in GameState.Singleton.partnerTrace) {
                    if (wasAssignmentBeaten(pt, click)) {
                        // We've been blocked BOO HISS.
                        wasBlockResolved = true;
                        GameState.Singleton.score -= blockPenalty;
                        string str = "-" + blockPenalty + " points!\n" + "(SELECTED SECOND)";
                        GUIUtils.SpawnFloatingText(TagUtils.GetPositionOfChildTag(this.gameObject, click.Third), str, Color.black, 2.0f);
                        Debug.Log(str);
                    }
                }
            }
        } else { // Tagged, but block hasn't been resolved.
            foreach (Triple<double, string, string> pt in GameState.Singleton.partnerTrace) {
                if (wasAssignmentBeaten(click, pt)) {
                    wasBlockResolved = true;
                    GameState.Singleton.score += blockPenalty;
                    string str = "+" + blockPenalty + " points!\n" + "(SELECTED FIRST)";
                    GUIUtils.SpawnFloatingText(TagUtils.GetPositionOfChildTag(this.gameObject, click.Third), str, Color.black, 2.0f);
                    Debug.Log(str);
                }
            }
        }
	}

    /// <summary></summary>
    /// <param name="c1"></param>
    /// <param name="c2"></param>
    /// <returns>True if c1 has the same assignment as and was faster than c2</returns>
    bool wasAssignmentBeaten(Triple<double, string, string> c1, Triple<double, string, string> c2) {
        return ((c1.First < c2.First) && (c1.Second == c2.Second) && (c1.Third == c2.Third));
    }
}
