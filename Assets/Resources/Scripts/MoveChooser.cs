using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;

public class MoveChooser : MonoBehaviour {

    private Rewired.Player rewPlayer; // The Rewired Player
    private int positionX = 0;
    private int positionY = 0;

    // Use this for initialization
    private void Awake() {
        rewPlayer = ReInput.players.GetPlayer(1);
    }

    void Start() {
        Rewired.Controller j = ReInput.controllers.GetController(ControllerType.Joystick, 0);
        rewPlayer.controllers.AddController(j, false);
    }

    // Update is called once per frame
    void Update () {

        Vector2 directionalInput = new Vector2(rewPlayer.GetAxis("Move Horizontal P2"), 0);
        
        if (directionalInput.x > 0 && positionX == 0) {
            // Move to right
            print("WE HERE");
            positionX++;
            transform.position = new Vector2(transform.position.x + 1.4f, transform.position.y);
        } else if (directionalInput.x < 0 && positionX == 1) {
            // Move to left
            positionX--;
            transform.position = new Vector2(transform.position.x - 1.4f, transform.position.y);
        }
    }
}
