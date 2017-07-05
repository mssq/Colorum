using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof (Controller2D))]
public class Player : PlayerManager {

    private CameraShake camShake;
    private LoadInformation loadInf;
    private SaveInformation saveInf;

    private Vector3 velocity;
    private float velocityXSmoothing;

    private Vector2 directionalInput;

    public float accelerationTimeAirborne = .2f;
    public float accelerationTimeGrounded = .1f;
    public float moveSpeed = 6;
    public float gravity = -20;
    public ParticleSystem deathParticle;

	protected override void Awake () {
        base.Awake();

        camShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
        loadInf = GameObject.FindGameObjectWithTag("Logic").GetComponent<LoadInformation>();
        saveInf = GameObject.FindGameObjectWithTag("Logic").GetComponent<SaveInformation>();
    }



    private void Update() {
        CalculateVelocity();
        //print("VELOCITY: " + velocity);
        controller.Move(velocity * Time.deltaTime, directionalInput);

        if (controller.collisions.above || controller.collisions.below) {
            velocity.y = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == "Threat") {
            camShake.Shake(0.08f, 0.25f);
            deathParticle.transform.position = this.transform.position;
            deathParticle.Play();
            StartCoroutine(Restart(1f));

            playerInput.enabled = false;
            sprite.enabled = false;
            coll.enabled = false;

        } else if (collider.tag == "Checkpoint") {
            Transform trans = collider.GetComponent<Transform>();
            //if (spawnLocation.transform.position != trans.position) {}

            // Move spawnLocation to position of this savepoint
            spawnLocation.transform.position = trans.position;
            // Save this information to the playerprefs.
            saveInf.SavePosInfromation();
            saveInf.SaveColorChanging(chooserInput.getColorState());
            saveInf.SaveGravityChanging(chooserInput.getGravityState());
            
        }
    }

    public void SetDirectionalInput (Vector2 input) {
        directionalInput = input;
    }

    public void onGravityInput() {
        if (controller.collisions.below || controller.collisions.above) {
            gravity *= -1;
        }
    }

    void CalculateVelocity() {
        float targetVelocityX = directionalInput.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing,
            (controller.collisions.below || controller.collisions.above) ? accelerationTimeGrounded : accelerationTimeAirborne);
        if (gravity > 0 && velocity.y > gravity || gravity < 0 && velocity.y < gravity) {
            velocity.y = gravity;
        } else {
            velocity.y += gravity * (Time.deltaTime * 2);
        }
    }

    public IEnumerator Restart(float waitTime) {

        yield return new WaitForSeconds(waitTime);

        loadInf.LoadAllInformation();

        // If player is upsidedown reset him
        if (Mathf.Sign(playerScript.gravity) == 1) {
            sprite.flipY = false;
            playerScript.ResetVelocity();
            coll.offset = new Vector2(coll.offset.x, coll.offset.y * -1);
            playerScript.gravity = -playerScript.gravity;
        }
        // Put player to spawn location
        playerTransform.position = spawnLocation.position;

        if (!playerInput.enabled)
            playerInput.enabled = true;
        if (!sprite.enabled)
            sprite.enabled = true;
        if (!coll.enabled)
            coll.enabled = true;

        yield return null;
    }

    public void ResetVelocity() {
        velocity = Vector3.zero;
    }

    public float getGravity() {
        return this.gravity;
    }

    public void SetGravity(float gravity) {
        if (this.gravity < 0) {
            this.gravity = -gravity;
        } else {
            this.gravity = gravity;
        }
        
    }
}
