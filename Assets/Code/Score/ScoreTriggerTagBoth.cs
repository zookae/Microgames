using UnityEngine;
using System.Collections;

public class ScoreTriggerTagBoth : MonoBehaviour
{
    private enum TagOptions
    {
        LabelMine,
        BlockPartner,
        AssignPartner
    }

    public float blockPenalty = 10;
    public float agreementBonus = 10;

    public bool wasTagged { get; private set; }
    public bool wasBothResolved { get; private set; }

    private TagOptions myTag;
    public Triple<double, string, string> click { get; private set; }

    // Use this for initialization
    void Start() {
        wasTagged = false;
        wasBothResolved = false;
        myTag = TagOptions.LabelMine; // THIS IS A HACK, but a necessary default
        click = null;
    }

    // Update is called once per frame
    void Update() {
        if ((wasBothResolved && wasTagged) || (GameState.Singleton.CurrentState != State.Running)) {
            return;
        }

        if (!wasTagged) {
            foreach (Triple<double, string, string> ct in GameState.Singleton.clickTrace) {
                if (ct.Second == transform.name) {
                    wasTagged = true;
                    this.click = ct;
                    if (click.Third.Contains("-collab")) {
                        myTag = TagOptions.AssignPartner;
                    } else if (click.Third.Contains("-compete")) {
                        myTag = TagOptions.BlockPartner;
                    } else {
                        myTag = TagOptions.LabelMine;
                    }

                    foreach (Triple<double, string, string> pt in GameState.Singleton.partnerTrace) {
                        // This prevents "future" clicks in the partner trace from firing immediately
                        // by breaking early.
                        if (pt.First >= this.click.First) {
                            break;
                        }
                        if (pt.Second == transform.name) { // FOUND IT
                            switch (myTag) {
                                case TagOptions.LabelMine:
                                    if (pt.Third.Contains("-collab")) {
                                        GameState.Singleton.score += agreementBonus;
                                        string strCollab = "+" + agreementBonus + " points!" + '\n' + "(AGREEMENT)";
                                        Debug.Log(strCollab);
                                    } else if (pt.Third.Contains("-compete")) {
                                        string strBlock = "-" + blockPenalty + " points!" + '\n' + "(SELECTED SECOND)";
                                        Debug.Log(strBlock);
                                    }
                                    break;
                                case TagOptions.AssignPartner:
                                    // They agreed with us.
                                    GameState.Singleton.score += agreementBonus;
                                    string strAgree = "+" + agreementBonus + " points!" + '\n' + "(AGREEMENT)";
                                    Debug.Log(strAgree);
                                    break;
                                case TagOptions.BlockPartner:
                                    // They beat us to it, so NOTHING HAPPENS :(
                                    string strBeaten = "+0 points!" + '\n' + "(SELECTED SECOND)";
                                    Debug.Log(strBeaten);
                                    break;
                                default:
                                    break; // WE SHOULD NEVER GET HERE
                            }
                            wasBothResolved = true;
                        }
                    }
                    break;
                }
            }
        } else { // wasTagged, but the block hasn't been resolved
            foreach (Triple<double, string, string> pt in GameState.Singleton.partnerTrace) {
                // Skips early traces.
                if (pt.First < this.click.First) {
                    continue;
                }
                if (pt.Second == transform.name) { // FOUND IT
                    switch (myTag) {
                        case TagOptions.LabelMine:
                            if (pt.Third.Contains("-collab")) {
                                GameState.Singleton.score += agreementBonus;
                                string strCollab = "+" + agreementBonus + " points!" + '\n' + "(AGREEMENT)";
                                Debug.Log(strCollab);
                            } else if (pt.Third.Contains("-compete")) {
                                // We beat them to it, but we don't gain/lose anything. Here's where they'd lose points.
                            }
                            break;
                        case TagOptions.AssignPartner:
                            // They agreed with us.
                            GameState.Singleton.score += agreementBonus;
                            string strAgree = "+" + agreementBonus + " points!" + '\n' + "(AGREEMENT)";
                            Debug.Log(strAgree);
                            break;
                        case TagOptions.BlockPartner:
                            // We beat them.
                            GameState.Singleton.score += blockPenalty;
                            string strBlock = "+" + blockPenalty + " points!" + '\n' + "(SELECTED FIRST)";
                            Debug.Log(strBlock);
                            break;
                        default:
                            break; // WE SHOULD NEVER GET HERE
                    }
                    wasBothResolved = true;
                }
            }
        }
    }

    /// <summary></summary>
    /// <param name="c1"></param>
    /// <param name="c2"></param>
    /// <returns>True if c1 has the same assignment as and was faster than c2</returns>
    bool wasAssignmentBeaten(Triple<double, string, string> c1, Triple<double, string, string> c2) {
        return ((c1.First < c2.First) && (c1.Second == c2.Second) && (TagUtils.TrimBothModeTag(c1.Third) == TagUtils.TrimBothModeTag(c2.Third)));
    }
}