using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;

[RequireComponent(typeof(Player))]
public class PlayerOneInput : MonoBehaviour {

    private Player player;
    private Rewired.Player rewPlayer; // The Rewired Player

    public int playerId; // The Rewired player id of this character

    private void Awake() {
        rewPlayer = ReInput.players.GetPlayer(playerId);
        player = GetComponent<Player>();
    }

    void Start() {
        Rewired.Controller j = ReInput.controllers.GetController(ControllerType.Joystick, 0);
        rewPlayer.controllers.AddController(j, false);
    }

    void Update() {

        Vector2 directionalInput = new Vector2(rewPlayer.GetAxisRaw("Move Horizontal P1"), 0);
        player.SetDirectionalInput(directionalInput);

        if (rewPlayer.GetButtonDown("Gravity")) {
            player.onGravityInput();
        }
    }

    public float getAxisHorizontal() {
        return rewPlayer.GetAxis("Move Horizontal P1");
    }

    public float getAxisVertical() {
        return rewPlayer.GetAxis("Move Vertical P1");
    }
}
