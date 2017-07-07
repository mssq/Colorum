using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

    public GameObject target;

    private float distanceX;
    private float distanceY;

    private void Start() {
        MoveCamera();
    }

    void Update () {
        MoveCamera();
    }

    private void MoveCamera() {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(target.transform.position);
        bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;

        distanceX = target.transform.position.x - this.transform.position.x;
        distanceY = target.transform.position.y - this.transform.position.y;

        if (!onScreen) {
            if (distanceX > 7) {
                // move right
                this.transform.position = new Vector3(this.transform.position.x + 14, this.transform.position.y, this.transform.position.z);
            } else if (distanceX < -7) {
                // move left
                this.transform.position = new Vector3(this.transform.position.x - 14, this.transform.position.y, this.transform.position.z);
            } else if (distanceY > 5) {
                // move up
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 11, this.transform.position.z);
            } else if (distanceY < -5) {
                // move down
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 11, this.transform.position.z);
            }
        }
    }
}
