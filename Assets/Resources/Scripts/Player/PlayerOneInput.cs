using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;

[RequireComponent(typeof(Player))]
public class PlayerOneInput : PlayerManager {

    void Start() {
        Rewired.Controller j = ReInput.controllers.GetController(ControllerType.Joystick, 0);
        rewPlayer.controllers.AddController(j, false);
        // Set player to right position at the beginning of the game
        StartCoroutine(playerScript.Restart(0f));
    }

    void Update() {

        Vector2 directionalInput = new Vector2(rewPlayer.GetAxisRaw("Move Horizontal P1"), 0);
        
        if (directionalInput.x > 0 || directionalInput.x < 0) {
            anim.SetBool("Walking", true);
            playerScript.SetDirectionalInput(directionalInput);
        } else if (directionalInput.x == 0) {
            anim.SetBool("Walking", false);
            playerScript.SetDirectionalInput(directionalInput);
        }

        if (directionalInput.x > 0) {
            sprite.flipX = false;

            /*if (coll.offset.x > 0) {
                coll.offset = new Vector2(coll.offset.x * -1, coll.offset.y);
            }*/

        } else if (directionalInput.x < 0) {
            sprite.flipX = true;

            /*if (coll.offset.x < 0) {
                coll.offset = new Vector2(coll.offset.x * -1, coll.offset.y);
            }*/
        }

        if (rewPlayer.GetButtonDown("Gravity")) {
            // If touching ground or ceiling
            if (controller.collisions.above || controller.collisions.below) {
                playerScript.onGravityInput();

                // Flip sprite and set collider offset
                sprite.flipY = !sprite.flipY;
                coll.offset = new Vector2(coll.offset.x, coll.offset.y * -1);
            }
        }

        if (rewPlayer.GetButtonDown("Restart")) {
            StartCoroutine(playerScript.Restart(0.2f));
        }
    }

    public float getAxisHorizontal() {
        return rewPlayer.GetAxis("Move Horizontal P1");
    }

    public float getAxisVertical() {
        return rewPlayer.GetAxis("Move Vertical P1");
    }

}
