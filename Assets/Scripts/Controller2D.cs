using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller2D : RaycastController {

    public CollisionInfo collisions;
    [HideInInspector]
    public Vector2 playerInput;

    public override void Start() {
        // Call the RaycastController start method
        base.Start();

    }

    public void Move(Vector2 moveAmount, Vector2 input, bool standingOnPlatform = false) {
        UpdateRaycastOrigins();
        collisions.Reset();
        playerInput = input;

        if (moveAmount.x != 0) {
            HorizontalCollisions(ref moveAmount);
        }
        if (moveAmount.y != 0) {
            VerticalCollisions(ref moveAmount);
        }

        transform.Translate(moveAmount);

        if (standingOnPlatform) {
            collisions.below = true;
        }
    }

    void HorizontalCollisions(ref Vector2 moveAmount) {
        float directionX = Mathf.Sign(moveAmount.x);
        float rayLength = Mathf.Abs(moveAmount.x) + skinWidth;

        for (int i = 0; i < horizontalRayCount; i++) {
            Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);
            // Draw the raycasts
            Debug.DrawRay(rayOrigin, Vector2.right * directionX, Color.red);

            if (hit) {
                
                if (hit.distance == 0) {
                    continue;
                }

                moveAmount.x = (hit.distance - skinWidth) * directionX;
                rayLength = hit.distance;

                collisions.left = directionX == -1;
                collisions.right = directionX == 1;
            }
        }
    }

    void VerticalCollisions(ref Vector2 moveAmount) {
        float directionY = Mathf.Sign(moveAmount.y);
        float rayLength = Mathf.Abs(moveAmount.y) + skinWidth;

        for (int i = 0; i < verticalRayCount; i++) {
            Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i + moveAmount.x);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);
            // Draw the raycasts
            Debug.DrawRay(rayOrigin, Vector2.up * directionY, Color.red);

            if (hit) {
                moveAmount.y = (hit.distance - skinWidth) * directionY;
                rayLength = hit.distance;

                collisions.below = directionY == -1;
                collisions.above = directionY == 1;
            }
        }
    }

    public struct CollisionInfo {
        public bool above, below;
        public bool left, right;

        public void Reset() {
            above = below = false;
            left = right = false;
        }
    }
}
