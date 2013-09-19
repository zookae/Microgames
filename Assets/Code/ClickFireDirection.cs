using UnityEngine;
using System.Collections;

public class ClickFireDirection : ShootInDirection {


    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            ShootInDir();
        }
    }
}
