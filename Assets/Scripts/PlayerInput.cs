using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

[RequireComponent (typeof (Player))]
public class PlayerInput : MonoBehaviour {

    Player player;

    public int playerId; // The Rewired player id of this character
    public GameObject lever;
    private Rewired.Player rewPlayer; // The Rewired Player

    void Start () {
        // Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
        rewPlayer = ReInput.players.GetPlayer(playerId);
        player = GetComponent<Player>();

        Rewired.Controller j = ReInput.controllers.GetController(ControllerType.Joystick, 0);
        rewPlayer.controllers.AddController(j, false);
    }

	void Update () {

        Vector2 directionalInput = new Vector2(rewPlayer.GetAxisRaw("Move Horizontal P" + (playerId + 1)), 0);
        player.SetDirectionalInput(directionalInput);

        if (rewPlayer.GetButtonDown("Gravity")) {
            player.onGravityInput();
        }

        /* ROTATION STUFF
        float angle = Mathf.Atan2(rewPlayer.GetAxis("Move Horizontal P2"), rewPlayer.GetAxis("Move Vertical P2")) * Mathf.Rad2Deg;
        lever.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -angle));
        print("ANGLE: " + angle); */
    }
}
