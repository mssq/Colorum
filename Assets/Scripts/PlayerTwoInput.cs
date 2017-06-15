using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;

[RequireComponent(typeof(Player))]
public class PlayerTwoInput : MonoBehaviour {

    private Player player;
    private Player playerOne;
    private GameObject playerGO;
    private Rewired.Player rewPlayer; // The Rewired Player
    private float origMoveSpeed;

    public int playerId; // The Rewired player id of this character
    public Lever[] levers;
    public Text leverOneText;

    private void Awake() {
        rewPlayer = ReInput.players.GetPlayer(playerId);
        player = GetComponent<Player>();
        playerGO = GameObject.FindGameObjectWithTag("Player One");
        playerOne = playerGO.GetComponent<Player>();
    }

    void Start() {
        Rewired.Controller j = ReInput.controllers.GetController(ControllerType.Joystick, 0);
        rewPlayer.controllers.AddController(j, false);
        origMoveSpeed = player.moveSpeed;
    }

    void Update() {

        Vector2 directionalInput = new Vector2(rewPlayer.GetAxisRaw("Move Horizontal P2"), 0);
        player.SetDirectionalInput(directionalInput);

        if (rewPlayer.GetButton("Pull Lever")) {
            for (int i = 0; i < levers.Length; i++) {
                if (levers[i].leverActivated) {
                    
                    // Lever one
                    if (i == 0) {
                        levers[i].PullLever();
                        if (playerOne.gravity == 5 || playerOne.gravity == -5) {
                            leverOneText.text = "LOW";
                        } else if (playerOne.gravity == 20 || playerOne.gravity == -20) {
                            leverOneText.text = "MED";
                        } else if (playerOne.gravity == 40 || playerOne.gravity == -40) {
                            leverOneText.text = "HIGH";
                        }
                    // Lever two
                    } else if (i == 1) {
                        levers[i].JoystickLever();
                    }
                    
                }
            }

        }

        if (rewPlayer.GetButtonUp("Pull Lever")) {
            player.moveSpeed = origMoveSpeed;
        }
    }

    public float getAxisHorizontal() {
        return rewPlayer.GetAxis("Move Horizontal P2");
    }

    public float getAxisVertical() {
        return rewPlayer.GetAxis("Move Vertical P2");
    }
}
