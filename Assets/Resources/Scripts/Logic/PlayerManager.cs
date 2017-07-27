using UnityEngine;
using System.Collections;
using Rewired;

public class PlayerManager : MonoBehaviour {

    [HideInInspector]
    protected Player playerScript;
    protected PlayerOneInput playerInput;
    protected GameObject playerObject;
    protected Transform playerTransform;
    protected Transform spawnLocation;
    protected Rewired.Player rewPlayer;
    protected Animator anim;
    protected SpriteRenderer sprite;
    protected Controller2D controller;
    protected BoxCollider2D coll;
    protected ChooserInput chooserInput;
    protected PlatformController platformController;
    protected GameObject gameLogic;

    protected Color yellow = new Color(0.898f, 0.785f, 0.102f);
    protected Color green = new Color(0.145f, 0.785f, 0.102f);
    protected Color red = new Color(0.784f, 0.102f, 0.149f);
    protected Color blue = new Color(0.129f, 0.165f, 0.986f);

    protected virtual void Awake() {
        playerObject = GameObject.FindGameObjectWithTag("Player One");
        playerScript = playerObject.GetComponent<Player>();
        playerInput = playerObject.GetComponent<PlayerOneInput>();
        coll = playerObject.GetComponent<BoxCollider2D>();
        rewPlayer = ReInput.players.GetPlayer(0);
        spawnLocation = GameObject.FindGameObjectWithTag("Spawn").GetComponent<Transform>();
        playerTransform = playerObject.GetComponent<Transform>();
        sprite = playerObject.GetComponent<SpriteRenderer>();
        controller = playerObject.GetComponent<Controller2D>();
        anim = playerObject.GetComponent<Animator>();
        chooserInput = GameObject.FindGameObjectWithTag("Player Two").GetComponent<ChooserInput>();
        platformController = GameObject.FindGameObjectWithTag("Moving Platform").GetComponent<PlatformController>();
        gameLogic = GameObject.FindGameObjectWithTag("Logic");
    }
}
