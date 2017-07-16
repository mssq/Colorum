﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class Controller : MonoBehaviour {

    private float positionX;
    private float positionY;

    private int buttonState = 0; // 0 = start, 1 = exit

    private Rewired.Player rewPlayer;
    private ButtonManager manager;

    private void Awake() {
        positionX = this.transform.position.x;
        positionY = this.transform.position.y;
        rewPlayer = ReInput.players.GetPlayer(0);
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<ButtonManager>();
    }

    void Start() {
        Rewired.Controller j = ReInput.controllers.GetController(ControllerType.Joystick, 0);
        rewPlayer.controllers.AddController(j, false);
    }

    private void Update() {
        Vector2 directionalInput = new Vector2(rewPlayer.GetAxis("Move Horizontal"), rewPlayer.GetAxis("Move Vertical"));
        
        if (directionalInput.y > 0.5 && buttonState == 1) {
            // Move down
            buttonState--;
            transform.position = new Vector2(transform.position.x, transform.position.y + 0.51f);
        } else if (directionalInput.y < -0.5 && buttonState == 0) {
            // Move up
            buttonState++;
            transform.position = new Vector2(transform.position.x, transform.position.y - 0.51f);
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