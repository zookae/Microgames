using UnityEngine;
using System.Collections;

public class MoveByKey : MonoBehaviour {

    public Rigidbody rbody;
    float force = 10.0f;

    public Vector3 maxVelocity = new Vector3(10,10,0);

	// Use this for initialization
	void Awake () {
        rbody = rigidbody;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        MoveByForceClamped();
	}


    void MoveByForce() {
        float xMovement = Input.GetAxis("Horizontal");
        float yMovement = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(xMovement, yMovement, 0);
        rbody.AddForce(dir * force, ForceMode.Force);
    }

    void MoveByForceClamped() {
        MoveByForce();
        Vector3 clamp = new Vector3(0, 0, 0);
        clamp.x = Mathf.Clamp(rbody.velocity.x, -maxVelocity.x, maxVelocity.x);
        clamp.y = Mathf.Clamp(rbody.velocity.y, -maxVelocity.y, maxVelocity.y);
        rbody.velocity = clamp;
    }
}
