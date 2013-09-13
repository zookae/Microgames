using UnityEngine;
using System.Collections;

public class CameraFollowLag : MonoBehaviour {

    Transform tr;
    public Transform target;
    public float damp = 5.0f;
    Vector3 newPos;

	// Use this for initialization
	void Awake () {
        tr = transform;
	}
	
	// Update is called once per frame
	void Update () {
        newPos = Vector3.Lerp(tr.position, target.position, Time.deltaTime * damp);
        newPos.z = tr.position.z;
        tr.position = newPos;
	}
}
