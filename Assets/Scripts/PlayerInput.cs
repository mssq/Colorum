using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

[RequireComponent (typeof (Player))]
public class PlayerInput : MonoBehaviour {

    Player player;

    public int playerId; // The Rewired player id of this character
    public Object playerTwo;
    private Rewired.Player rewPlayer; // The Rewired Player

    void Start () {
        // Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
        rewPlayer = ReInput.players.GetPlayer(playerId);
        player = GetComponent<Player>();

        Rewired.Controller j = ReInput.controllers.GetController(ControllerType.Joystick, 0);
        rewPlayer.controllers.AddController(j, false);
    }

	void Update () {

       Vector2 directionalInput = new Vector2(rewPlayer.GetAxisRaw("Move Horizontal"), 0);
       player.SetDirectionalInput(directionalInput);

       if (rewPlayer.GetButtonDown("Gravity")) {
            player.onGravityInput();
       }
    }
}
