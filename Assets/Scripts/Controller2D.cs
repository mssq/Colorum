using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (BoxCollider2D))]
public class Controller2D : MonoBehaviour {

    // This determines how deep inside the player the raycasts will be projected
    const float skinWidth = .015f;

    // How many rays will be projected
    public int horizontalRayCount = 4;
    public int verticalRayCount = 4;

    // Spacing between rays
    float horizontalRaySpacing;
    float verticalRaySpacing;

    BoxCollider2D playerCollider;
    RaycastOrigins raycastOrigins;

    void Start() {
        playerCollider = GetComponent<BoxCollider2D>();
    }

    void Update() {
        UpdateRaycastOrigins();
        CalculateRaySpacing();

        // Draw the raycasts
        for (int i = 0; i < verticalRayCount; i++) {
            Debug.DrawRay(raycastOrigins.bottomLeft + Vector2.right * verticalRaySpacing * i,
                Vector2.up * -2, Color.red);
        }
    }

    void UpdateRaycastOrigins() {
        Bounds bounds = playerCollider.bounds;
        bounds.Expand(skinWidth * -2);

        raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    void CalculateRaySpacing() {
        Bounds bounds = playerCollider.bounds;
        bounds.Expand(skinWidth * -2);

        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
        verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);

        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);

    }

    // Raycast location origins
    struct RaycastOrigins {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }
}
