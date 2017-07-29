using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class Controller : MonoBehaviour {

    private int buttonState = 0; // 0 = start, 1 = exit

    private Rewired.Player rewPlayer;
    private ButtonManager manager;

    private SpriteRenderer playerSpr;
    public GameObject player;

    private void Awake() {
        rewPlayer = ReInput.players.GetPlayer(0);
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<ButtonManager>();
        playerSpr = player.GetComponent<SpriteRenderer>();
    }

    void Start() {
        Rewired.Controller j = ReInput.controllers.GetController(ControllerType.Joystick, 0);
        rewPlayer.controllers.AddController(j, false);
    }

    private void Update() {
        Vector2 directionalInput = new Vector2(rewPlayer.GetAxis("Move Horizontal"), rewPlayer.GetAxis("Move Vertical"));

        if (directionalInput.y > 0.5 && buttonState == 1 || rewPlayer.GetButton("Move Up") && buttonState == 1) {
            // Move down
            buttonState--;
            transform.position = new Vector3(transform.position.x - 0.15f, transform.position.y + 0.53f, transform.position.z);
            playerSpr.flipX = false;
        } else if (directionalInput.y < -0.5 && buttonState == 0 || rewPlayer.GetButton("Move Down") && buttonState == 0) {
            // Move up
            buttonState++;
            transform.position = new Vector3(transform.position.x + 0.15f, transform.position.y - 0.53f, transform.position.z);
            playerSpr.flipX = true;
        }

        if (rewPlayer.GetButton("Action")) {
            if (buttonState == 0) {
                manager.StartBtn("Game");
            } else if (buttonState == 1) {
                manager.ExitBtn();
            }
        }
    }

}
