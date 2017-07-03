using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;

[RequireComponent(typeof(Player))]
public class PlayerOneInput : MonoBehaviour {

    private Player player;
    private Rewired.Player rewPlayer; // The Rewired Player
    private SpriteRenderer sprite;
    private BoxCollider2D coll;
    private Animator anim;
    private Controller2D controller;

    public int playerId; // The Rewired player id of this character

    private void Awake() {
        rewPlayer = ReInput.players.GetPlayer(playerId);
        player = GetComponent<Player>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        controller = GetComponent<Controller2D>();
    }

    void Start() {
        Rewired.Controller j = ReInput.controllers.GetController(ControllerType.Joystick, 0);
        rewPlayer.controllers.AddController(j, false);
    }

    void Update() {

        Vector2 directionalInput = new Vector2(rewPlayer.GetAxisRaw("Move Horizontal P1"), 0);
        
        if (directionalInput.x > 0 || directionalInput.x < 0) {
            anim.SetBool("Walking", true);
            player.SetDirectionalInput(directionalInput);
        } else if (directionalInput.x == 0) {
            anim.SetBool("Walking", false);
            player.SetDirectionalInput(directionalInput);
        }

        if (directionalInput.x > 0) {
            sprite.flipX = false;
        } else if (directionalInput.x < 0) {
            sprite.flipX = true;
        }

        if (rewPlayer.GetButtonDown("Gravity")) {
            // If touching ground or ceiling
            if (controller.collisions.above || controller.collisions.below) {
                player.onGravityInput();

                // Flip sprite and set collider offset
                sprite.flipY = !sprite.flipY;
                coll.offset = new Vector2(coll.offset.x, coll.offset.y * -1);
            }
        }
    }

    public float getAxisHorizontal() {
        return rewPlayer.GetAxis("Move Horizontal P1");
    }

    public float getAxisVertical() {
        return rewPlayer.GetAxis("Move Vertical P1");
    }
}
