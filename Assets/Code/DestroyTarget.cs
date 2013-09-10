﻿using UnityEngine;
using System.Collections;

public class DestroyTarget : MonoBehaviour {

    public string tag;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col) {
        if (col.CompareTag(tag)) {
            Destroy(col.gameObject);
        }
        
    }
}