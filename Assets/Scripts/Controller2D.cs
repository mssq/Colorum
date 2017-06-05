using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (BoxCollider2D))]
public class Controller2D : MonoBehaviour {

    // Reference to the player collider
    BoxCollider2D playerCollider;

    void Start() {
        playerCollider = GetComponent<BoxCollider2D>();
    }
}
