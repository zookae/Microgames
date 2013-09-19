using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class MoveInDirection : MoveControl {

    public MoveDirection dir;

	// Update is called once per frame
	void Update () {
        if (isRunning) {
            switch (dir) {
                case MoveDirection.Up:
                    //this.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.up * moveRate);
                    transform.position += transform.up * moveRate * Time.deltaTime;
                    break;
                case MoveDirection.Down:
                    //this.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.down * moveRate);
                    transform.position -= transform.up * moveRate * Time.deltaTime;
                    break;
                case MoveDirection.Right:
                    //this.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.right * moveRate);
                    transform.position += transform.right * moveRate * Time.deltaTime;
                    break;
                case MoveDirection.Left:
                    //this.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.left * moveRate);
                    transform.position -= transform.right * moveRate * Time.deltaTime;
                    break;
            }
        }
	}
}
