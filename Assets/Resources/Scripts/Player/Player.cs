using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent (typeof (Controller2D))]
public class Player : PlayerManager {

    private CameraShake camShake;
    private LoadInformation loadInf;
    private SaveInformation saveInf;

    private AudioSource[] musics;
    private AudioSource bgMusic;
    private AudioSource bossMusic;

    private AudioSource[] sounds;
    private AudioSource jmpSound;
    private AudioSource deathSound;
    private AudioSource saveSound;

    private Vector3 velocity;
    private float velocityXSmoothing;

    private Vector2 directionalInput;

    private GameObject[] checkpoints;

    public float accelerationTimeAirborne = .2f;
    public float accelerationTimeGrounded = .1f;
    public float moveSpeed = 6;
    public float gravity = -20;
    public ParticleSystem deathParticle;
    public GameObject castleSevenBlock;
    public GameObject uSpikes;
    public GameObject boss;
    public GameObject end;

	protected override void Awake () {
        base.Awake();

        camShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
        loadInf = GameObject.FindGameObjectWithTag("Logic").GetComponent<LoadInformation>();
        saveInf = GameObject.FindGameObjectWithTag("Logic").GetComponent<SaveInformation>();
        checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
        sounds = GetComponents<AudioSource>();
        jmpSound = sounds[0];
        deathSound = sounds[1];
        saveSound = sounds[2];
        musics = gameLogic.GetComponents<AudioSource>();
        bgMusic = musics[0];
        bossMusic = musics[1];
    }



    private void Update() {
        CalculateVelocity();
        //print("VELOCITY: " + velocity);
        controller.Move(velocity * Time.deltaTime, directionalInput);

        if (controller.collisions.above || controller.collisions.below) {

            if (platformController.passengerMovement.Count == 0) {
                velocity.y = 0;
            }

            if (anim.GetBool("Grounded") == false) {
                anim.SetBool("Grounded", true);
            }
        } else if (!controller.collisions.above || !controller.collisions.below) {
            if (anim.GetBool("Grounded") == true) {
                anim.SetBool("Grounded", false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Disappear") {
            if (castleSevenBlock.activeInHierarchy) {
                castleSevenBlock.SetActive(false);
                uSpikes.SetActive(false);
                boss.SetActive(false);
            }
        } else if (collision.tag == "Appear") {
            if (!castleSevenBlock.activeInHierarchy) {
                castleSevenBlock.SetActive(true);
                uSpikes.SetActive(true);
                boss.SetActive(true);
                if (bgMusic.isPlaying) 
                    bgMusic.Stop();
                if (!bossMusic.isPlaying)
                    bossMusic.Play();
            }
            
        } else if (collision.tag == "End") {
            playerInput.enabled = false;
            sprite.enabled = false;

            StartCoroutine(BackToMenu(3f));
        }
    }

    private void OnTriggerStay2D(Collider2D collider) {
        if (collider.tag == "Threat") {
            if (collider.GetComponent<SpriteRenderer>().color != sprite.color) {
                camShake.Shake(0.08f, 0.25f);

                deathParticle.transform.position = this.transform.position;
                deathParticle.Play();

                if (!deathSound.isPlaying)
                    deathSound.Play();
                if (bossMusic.isPlaying)
                    bossMusic.Stop();
                if (!bgMusic.isPlaying)
                    bgMusic.PlayDelayed(0.75f);

                StartCoroutine(Restart(1f));
                playerInput.enabled = false;
                coll.enabled = false;
            }
        } else if (collider.tag == "Checkpoint") {
            Transform trans = collider.GetComponent<Transform>();

            if (PlayerPrefs.GetInt("COLOR") != chooserInput.getColorState() ||
                PlayerPrefs.GetInt("GRAVITY") != chooserInput.getGravityState() ||
                spawnLocation.transform.position.x != trans.position.x) {

                getCheckpoints(true);
                if (!saveSound.isPlaying)
                    saveSound.Play();

                // Move spawnLocation to position of this savepoint
                spawnLocation.transform.position = new Vector3(trans.position.x, trans.position.y - 0.3f, trans.position.z);

                // Save this information to the playerprefs.
                saveInf.SavePosInfromation();
                saveInf.SaveColorChanging(chooserInput.getColorState());
                saveInf.SaveGravityChanging(chooserInput.getGravityState());
            }
        }
    }

    // Get all checkpoints and stop or play animation
    private void getCheckpoints(bool activated) {

        foreach (GameObject checkpoint in checkpoints) {
            Animator cpAnim = checkpoint.GetComponent<Animator>();
            cpAnim.SetBool("Activated", activated);
        }
    }

    private IEnumerator BackToMenu(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("MainMenu");
        yield return null;
    }

    private IEnumerator WaitForAnimation(Animation animation) {
        do {
            yield return null;
        } while (animation.isPlaying);
    }

    public void SetDirectionalInput (Vector2 input) {
        directionalInput = input;
    }

    public void onGravityInput() {
        if (controller.collisions.below || controller.collisions.above) {
            gravity *= -1;
            if (!jmpSound.isPlaying)
                jmpSound.Play();
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

        sprite.enabled = false;

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
