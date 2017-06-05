using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Controller2D))]
public class Player : MonoBehaviour {

    public float accelerationTimeAirborne = .2f;
    public float accelerationTimeGrounded = .1f;
    public float moveSpeed = 6;
    public float gravity = -20;

    Vector3 velocity;
    float velocityXSmoothing;

    // Reference to the player controller script
    Controller2D controller;

	void Start () {
        controller = GetComponent<Controller2D>();
	}
	
	void Update () {

        if (controller.collisions.above || controller.collisions.below) {
            velocity.y = 0;
        }

        // Get button direction
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // Invert gravity
        if (Input.GetKeyDown(KeyCode.Space) && controller.collisions.below ||
            Input.GetKeyDown(KeyCode.Space) && controller.collisions.above) {
            gravity *= -1;
        }

        float targetVelocityX = input.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, 
            (controller.collisions.below || controller.collisions.above) ? accelerationTimeGrounded : accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
	}
}
