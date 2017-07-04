using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;

[RequireComponent(typeof(Player))]
public class PlayerOneInput : PlayerManager {

    private LoadInformation loadInf;

    void Start() {
        Rewired.Controller j = ReInput.controllers.GetController(ControllerType.Joystick, 0);
        rewPlayer.controllers.AddController(j, false);
        // Set player to right position at the beginning of the game
        StartCoroutine(Restart(0f));
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
        } else if (directionalInput.x < 0) {
            sprite.flipX = true;
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
            StartCoroutine(Restart(0.2f));
        }
    }

    public float getAxisHorizontal() {
        return rewPlayer.GetAxis("Move Horizontal P1");
    }

    public float getAxisVertical() {
        return rewPlayer.GetAxis("Move Vertical P1");
    }

    IEnumerator Restart(float waitTime) {

        yield return new WaitForSeconds(waitTime);

        if (loadInf == null) {
            loadInf = gameObject.AddComponent<LoadInformation>();
        }

        loadInf.LoadAllInformation();

        // If player is upsidedown reset him
        if (Mathf.Sign(playerScript.gravity) == 1) {
            sprite.flipY = false;
            playerScript.ResetVelocity();
            coll.offset = new Vector2(coll.offset.x, coll.offset.y * -1);
            playerScript.gravity = -playerScript.gravity;
        }
        // Put player to spawn location
        playerTransform.position = spawnLocation.position;

        if (playerObject.activeInHierarchy == false) {
            playerObject.SetActive(true);
        }

        yield return null;
    }
}
