using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class Lever : MonoBehaviour {

    private GameObject playerTwo;
    private float angleLimit = 85;

    public Player player;
    public PlayerInput pInput;
    public bool leverActivated = false;

    private void Awake() {
        playerTwo = GameObject.FindGameObjectWithTag("Player Two");
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player Two") {
            leverActivated = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player Two") {
            leverActivated = false;
        }
    }

    public void PullLever() {
        if (leverActivated) {
            player.moveSpeed = 0;
            float angle = Mathf.Atan2(pInput.getP2AxisHorizontal(), pInput.getP2AxisVertical()) * Mathf.Rad2Deg;

            if (angle > angleLimit) {
                this.transform.parent.rotation = Quaternion.Euler(new Vector3(0, 0, -angleLimit));
            } else if (angle < -angleLimit) {
                this.transform.parent.rotation = Quaternion.Euler(new Vector3(0, 0, angleLimit));
            } else {
                this.transform.parent.rotation = Quaternion.Euler(new Vector3(0, 0, -angle));
            }

            playerTwo.transform.position = new Vector2(this.transform.position.x, playerTwo.transform.position.y);
        }
    }
}
