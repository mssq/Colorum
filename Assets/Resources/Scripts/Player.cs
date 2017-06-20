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

    Vector2 directionalInput;

    // Reference to the player controller script
    Controller2D controller;

	void Awake () {
        controller = GetComponent<Controller2D>();
	}

    public void SetDirectionalInput (Vector2 input) {
        directionalInput = input;
    }

    public void onGravityInput() {
        if (controller.collisions.below || controller.collisions.above) {
            gravity *= -1;
        }
    }
	
	void Update () {
        CalculateVelocity();

        controller.Move(velocity * Time.deltaTime, directionalInput);

        if (controller.collisions.above || controller.collisions.below) {
            velocity.y = 0;
        }
    }

    void CalculateVelocity() {
        float targetVelocityX = directionalInput.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing,
            (controller.collisions.below || controller.collisions.above) ? accelerationTimeGrounded : accelerationTimeAirborne);
        if (gravity > 0 && velocity.y > gravity || gravity < 0 && velocity.y < gravity) {
            velocity.y = gravity;
        } else {
            velocity.y += gravity * Time.deltaTime;
        }
    }

    public float getGravity() {
        return this.gravity;
    }

    public void setGravity(float gravity) {
        if (this.gravity < 0) {
            this.gravity = -gravity;
        } else {
            this.gravity = gravity;
        }
        
    }
}
