using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

[RequireComponent (typeof (Player))]
public class PlayerInput : MonoBehaviour {

    Player player;

    public int playerId = 0; // The Rewired player id of this character
    private Rewired.Player rewPlayer; // The Rewired Player

    void Start () {
        // Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
        rewPlayer = ReInput.players.GetPlayer(playerId);
        player = GetComponent<Player>();
	}

	void Update () {
       Vector2 directionalInput = new Vector2(rewPlayer.GetAxisRaw("Move Horizontal"), rewPlayer.GetAxisRaw("Move Vertical"));
       player.SetDirectionalInput(directionalInput);

       if (rewPlayer.GetButtonDown("Gravity")) {
            player.onGravityInput();
       }
    }
}
