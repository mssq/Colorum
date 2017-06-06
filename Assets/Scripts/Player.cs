using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

[RequireComponent (typeof (Controller2D))]
public class Player : MonoBehaviour {

    public int playerId = 1; // The Rewired player id of this character
    private Rewired.Player player; // The Rewired Player

    public float accelerationTimeAirborne = .2f;
    public float accelerationTimeGrounded = .1f;
    public float moveSpeed = 6;
    public float gravity = -20;

    Vector3 velocity;
    float velocityXSmoothing;

    // Reference to the player controller script
    Controller2D controller;

	void Awake () {
        // Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
        player = ReInput.players.GetPlayer(playerId);
        controller = GetComponent<Controller2D>();
	}
	
	void Update () {

        if (controller.collisions.above || controller.collisions.below) {
            velocity.y = 0;
        }

        // Get button direction
        Vector2 input = new Vector2(player.GetAxisRaw("Move Horizontal"), player.GetAxisRaw("Move Vertical"));

        // Invert gravity
        if (player.GetButtonDown("Gravity") && controller.collisions.below ||
            player.GetButtonDown("Gravity") && controller.collisions.above) {
            gravity *= -1;
        }

        float targetVelocityX = input.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, 
            (controller.collisions.below || controller.collisions.above) ? accelerationTimeGrounded : accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
	}
}
