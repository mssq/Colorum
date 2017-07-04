using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;

public class ChooserInput : PlayerManager {

    private int positionX = 0;
    private int positionY = 0;
    private int gravityState = 1; // 0 = low, 1 = med, 2 = high
    private int colorState = 0; // 0 = red, 1 = blue, 2 = green, 3 = yellow
    private bool resetJoystick = false;
    private GameObject playerOne;
    private Player player;
    private SpriteRenderer chooserSprite;
    private SpriteRenderer playerSprite;

    private SpriteRenderer rArrowSprite;
    private SpriteRenderer lArrowSprite;
    public GameObject rArrow;
    public GameObject lArrow;

    private Animator animJoystick;
    public GameObject arcadeJoystick;

    public Image gravityImage;
    public Image colorImage;
    

    protected override void Awake() {
        base.Awake();

        rewPlayer = ReInput.players.GetPlayer(1);
        chooserSprite = GetComponent<SpriteRenderer>();
        rArrowSprite = rArrow.GetComponent<SpriteRenderer>();
        lArrowSprite = lArrow.GetComponent<SpriteRenderer>();
        animJoystick = arcadeJoystick.GetComponent<Animator>();
    }

    void Start() {
        Rewired.Controller j = ReInput.controllers.GetController(ControllerType.Joystick, 0);
        rewPlayer.controllers.AddController(j, false);
    }

    void Update () {

        Vector2 directionalInput = new Vector2(rewPlayer.GetAxis("Move Horizontal P2"), rewPlayer.GetAxis("Move Vertical P2"));

        if (rewPlayer.GetButton("Pull Lever")) {
            chooserSprite.enabled = false;
            interact(directionalInput);
        } else {
            if (directionalInput.x > 0.5 && positionX == 0) {
                // Move right
                positionX++;
                transform.position = new Vector2(transform.position.x + 1.358f, transform.position.y);
                lArrow.transform.position = new Vector2(lArrow.transform.position.x + 1.385f, lArrow.transform.position.y);
                rArrow.transform.position = new Vector2(rArrow.transform.position.x + 1.385f, rArrow.transform.position.y);
                animJoystick.SetInteger("Position", 1);
            } else if (directionalInput.x < -0.5 && positionX == 1) {
                // Move left
                positionX--;
                transform.position = new Vector2(transform.position.x - 1.358f, transform.position.y);
                lArrow.transform.position = new Vector2(lArrow.transform.position.x - 1.385f, lArrow.transform.position.y);
                rArrow.transform.position = new Vector2(rArrow.transform.position.x - 1.385f, rArrow.transform.position.y);
                animJoystick.SetInteger("Position", 2);
            } else if (directionalInput.y > 0.5 && positionY == 1) {
                // Move up
                positionY--;
                transform.position = new Vector2(transform.position.x, transform.position.y + 2.13f);
                lArrow.transform.position = new Vector2(lArrow.transform.position.x, lArrow.transform.position.y + 2.13f);
                rArrow.transform.position = new Vector2(rArrow.transform.position.x, rArrow.transform.position.y + 2.13f);
            } else if (directionalInput.y < -0.5 && positionY == 0) {
                // Move down
                positionY++;
                transform.position = new Vector2(transform.position.x, transform.position.y - 2.13f);
                lArrow.transform.position = new Vector2(lArrow.transform.position.x, lArrow.transform.position.y - 2.13f);
                rArrow.transform.position = new Vector2(rArrow.transform.position.x, rArrow.transform.position.y - 2.13f);
            } else if (directionalInput.x < 0.2 && directionalInput.x > - 0.2) {
                animJoystick.SetInteger("Position", 0);
            }
        }

        if (rewPlayer.GetButtonUp("Pull Lever")) {
            chooserSprite.enabled = true;
            lArrowSprite.enabled = false;
            rArrowSprite.enabled = false;
        }
    }

    // When player presses button, interact with arcade machine and change player ones values
    private void interact(Vector2 input) {

        showArrow();

        if (input.x < 0.2 && input.x > -0.2) {
            resetJoystick = false;
            animJoystick.SetInteger("Position", 0);
        }

        if (positionX == 0 && positionY == 0) {
            // Top left
            if (input.x > 0.5 && !resetJoystick) {
                animJoystick.SetInteger("Position", 1);

                switch (gravityState) {
                    case 0:
                        gravityImage.sprite = Resources.Load<Sprite>("Sprites/GravityIconMed");
                        playerScript.SetGravity(5.5f);
                        gravityState++;
                        break;
                    case 1:
                        gravityImage.sprite = Resources.Load<Sprite>("Sprites/GravityIconHigh");
                        playerScript.SetGravity(8);
                        gravityState++;
                        break;
                    default:
                        break;
                }
                resetJoystick = true;

            } else if (input.x < -0.5 && !resetJoystick) {
                animJoystick.SetInteger("Position", 2);

                switch (gravityState) {
                    case 1:
                        gravityImage.sprite = Resources.Load<Sprite>("Sprites/GravityIconLow");
                        playerScript.SetGravity(3);
                        gravityState--;
                        break;
                    case 2:
                        gravityImage.sprite = Resources.Load<Sprite>("Sprites/GravityIconMed");
                        playerScript.SetGravity(5.5f);
                        gravityState--;
                        break;
                    default:
                        break;
                }
                resetJoystick = true;
            }

        } else if (positionX == 1 && positionY == 0) {
            // Top right
            if (input.x > 0.5 && !resetJoystick) {
                animJoystick.SetInteger("Position", 1);

                switch (colorState) {
                    case 0:
                        sprite.color = blue;
                        colorImage.sprite = Resources.Load<Sprite>("Sprites/PlayerColorBlue");
                        colorState++;
                        break;
                    case 1:
                        sprite.color = green;
                        colorImage.sprite = Resources.Load<Sprite>("Sprites/PlayerColorGreen");
                        colorState++;
                        break;
                    case 2:
                        sprite.color = yellow;
                        colorImage.sprite = Resources.Load<Sprite>("Sprites/PlayerColorYellow");
                        colorState++;
                        break;
                    default:
                        break;
                }
                resetJoystick = true;

            } else if (input.x < -0.5 && !resetJoystick) {
                animJoystick.SetInteger("Position", 2);

                switch (colorState) {
                    case 1:
                        sprite.color = red;
                        colorImage.sprite = Resources.Load<Sprite>("Sprites/PlayerColorRed");
                        colorState--;
                        break;
                    case 2:
                        sprite.color = blue;
                        colorImage.sprite = Resources.Load<Sprite>("Sprites/PlayerColorBlue");
                        colorState--;
                        break;
                    case 3:
                        sprite.color = green;
                        colorImage.sprite = Resources.Load<Sprite>("Sprites/PlayerColorGreen");
                        colorState--;
                        break;
                    default:
                        break;
                }
                resetJoystick = true;
            }

        } else if (positionX == 0 && positionY == 1) {
            // Bottom left

        } else if (positionX == 1 && positionY == 1) {
            // Bottom right

        }
    }

    private void showArrow() {
        if (positionX == 0 && positionY == 0) {
            switch (gravityState) {
                case 0:
                    lArrowSprite.enabled = false;
                    rArrowSprite.enabled = true;
                    break;
                case 1:
                    lArrowSprite.enabled = true;
                    rArrowSprite.enabled = true;
                    break;
                case 2:
                    lArrowSprite.enabled = true;
                    rArrowSprite.enabled = false;
                    break;
            }
        } else if (positionX == 1 && positionY == 0) {
            switch (colorState) {
                case 0:
                    lArrowSprite.enabled = false;
                    rArrowSprite.enabled = true;
                    break;
                case 1:
                    lArrowSprite.enabled = true;
                    rArrowSprite.enabled = true;
                    break;
                case 2:
                    lArrowSprite.enabled = true;
                    rArrowSprite.enabled = true;
                    break;
                case 3:
                    lArrowSprite.enabled = true;
                    rArrowSprite.enabled = false;
                    break;
            }
        }
        
    }
}
