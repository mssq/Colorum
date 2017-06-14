using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class Lever : MonoBehaviour {

    private GameObject playerTwo;
    private PlayerTwoInput pInput;

    public Player playerOneScript;
    public Player playerTwoScript;
    public bool leverActivated = false;

    private void Awake() {
        playerTwo = GameObject.FindGameObjectWithTag("Player Two");
        pInput = playerTwo.GetComponent<PlayerTwoInput>();
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
            playerTwoScript.moveSpeed = 0;
            float angle = Mathf.Atan2(pInput.getAxisHorizontal(), pInput.getAxisVertical()) * Mathf.Rad2Deg;
            float distance = new Vector2(pInput.getAxisHorizontal(), pInput.getAxisVertical()).magnitude;

            if (distance > 0.6) {
                if (angle < -30) {
                    // LOW
                    this.transform.parent.rotation = Quaternion.Euler(new Vector3(0, 0, 45));
                    playerOneScript.gravity = (playerOneScript.gravity > 0) ? 5 : -5;
                } else if (angle > 30) {
                    // HIGH
                    this.transform.parent.rotation = Quaternion.Euler(new Vector3(0, 0, -45));
                    playerOneScript.gravity = (playerOneScript.gravity > 0) ? 40 : -40;
                } else {
                    // MED
                    this.transform.parent.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    playerOneScript.gravity = (playerOneScript.gravity > 0) ? 20 : -20;
                }
            }
            print("DISTANCE: " + distance);
            playerTwo.transform.position = new Vector2(this.transform.position.x, playerTwo.transform.position.y);
        }
    }
}
