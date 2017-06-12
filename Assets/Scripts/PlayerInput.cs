using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

[RequireComponent (typeof (Player))]
public class PlayerInput : MonoBehaviour {

    private Player player;
    private Rewired.Player rewPlayer; // The Rewired Player
    private float origMoveSpeed;

    public int playerId; // The Rewired player id of this character
    public Lever[] levers;

    private void Awake() {
        rewPlayer = ReInput.players.GetPlayer(playerId);
        player = GetComponent<Player>();
    }

    void Start () {
        Rewired.Controller j = ReInput.controllers.GetController(ControllerType.Joystick, 0);
        rewPlayer.controllers.AddController(j, false);
        origMoveSpeed = player.moveSpeed;
    }

	void Update () {

        Vector2 directionalInput = new Vector2(rewPlayer.GetAxisRaw("Move Horizontal P" + (playerId + 1)), 0);
        player.SetDirectionalInput(directionalInput);

        if (rewPlayer.GetButtonDown("Gravity")) {
            player.onGravityInput();
        }

        if (rewPlayer.GetButton("Pull Lever")) {
            for (int i = 0; i < levers.Length; i++) {
                if (levers[i].leverActivated) {
                    levers[i].PullLever();
                }
            }
            
        }

        if (rewPlayer.GetButtonUp("Pull Lever")) {
            player.moveSpeed = origMoveSpeed;
        }
    }

    public float getP2AxisHorizontal() {
        return rewPlayer.GetAxis("Move Horizontal P2");
    }

    public float getP2AxisVertical() {
        return rewPlayer.GetAxis("Move Vertical P2");
    }
}
