using UnityEngine;
using System.Collections;


public class MoveInDirection : MoveControl {

    public MoveDirection dir;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (isRunning) {
            switch (dir) {
                case MoveDirection.Up:
                    transform.position += transform.up * moveRate * Time.deltaTime;
                    break;
                case MoveDirection.Down:
                    transform.position -= transform.up * moveRate * Time.deltaTime;
                    break;
                case MoveDirection.Right:
                    transform.position += transform.right * moveRate * Time.deltaTime;
                    break;
                case MoveDirection.Left:
                    transform.position -= transform.right * moveRate * Time.deltaTime;
                    break;
            }
        }
	}
}
