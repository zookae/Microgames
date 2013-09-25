using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrackClick : MonoBehaviour {

    public List<Pair<GameObject, float>> clickTrace;

	// Use this for initialization
	void Start () {
        clickTrace = new List<Pair<GameObject, float>>();
	}
	
}
