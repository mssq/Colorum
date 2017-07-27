using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFight : MonoBehaviour {

    public GameObject[] bulletEmitter;
    public GameObject bullet;
    public GameObject player;
    public float bulletSpeed;
    public float bulletDelay;
    public int bulletMode = 0; // 1 = I, 2 = X, 3 = SPIKEDOWN, 4 = SPIKEUP
    public int currentColor = 0; // 0 = RED, 1 = BLUE, 2 = GREEN, 3 = YELLOW
    public float destroyTime = 2.5f;
    public float bossTimer;
    public GameObject door;

    public GameObject dSpikes;
    private Animator dSpikesAnim;
    public GameObject uSpikes;
    private Animator uSpikesAnim;

    private AudioSource[] sounds;
    private AudioSource roarSound;
    private AudioSource hitSound;
    private AudioSource shootSound;

    private float timer;
    private Animator anim;
    private Renderer rend;
    private SpriteRenderer playerSpr;
    private CameraShake camShake;

    private Color yellow = new Color(0.898f, 0.785f, 0.102f);
    private Color green = new Color(0.145f, 0.785f, 0.102f);
    private Color red = new Color(0.784f, 0.102f, 0.149f);
    private Color blue = new Color(0.129f, 0.165f, 0.986f);

    private void Awake() {
        anim = GetComponent<Animator>();
        rend = GetComponent<Renderer>();
        dSpikesAnim = dSpikes.GetComponent<Animator>();
        uSpikesAnim = uSpikes.GetComponent<Animator>();
        playerSpr = player.GetComponent<SpriteRenderer>();
        camShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShake>();
        sounds = GetComponents<AudioSource>();
        roarSound = sounds[0];
        hitSound = sounds[1];
        shootSound = sounds[2];
    }

    private void Update() {
        timer += Time.deltaTime;
        bossTimer += Time.deltaTime;

        if (bossTimer > 40f) {
            bulletMode = 5;
        } else if (bossTimer > 33f) {
            bulletMode = 1;
        } else if (bossTimer > 25f) {
            bulletMode = 2;
        } else if (bossTimer > 22f) {
            bulletMode = 4;
        } else if (bossTimer > 20f) {
            bulletMode = 3;
        } else if (bossTimer > 11f) {
            bulletMode = 2;
        } else if (bossTimer > 5f) {
            bulletMode = 1;
        } else if (bossTimer > 1f) {
            bulletMode = 3;
        }

        if (timer > bulletDelay) {

            currentColor = Random.Range(0, 4);
            rend.material.SetColor("_Colorout", ChangeColor());

            if (bulletMode == 0) {
                anim.SetInteger("AnimState", 6);
            } else if (bulletMode == 1) {
                anim.SetInteger("AnimState", 1);
                IBullets();
            } else if (bulletMode == 2) {
                anim.SetInteger("AnimState", 2);
                XBullets();
            } else if (bulletMode == 3) {
                anim.SetInteger("AnimState", 3);  
            } else if (bulletMode == 4) {
                anim.SetInteger("AnimState", 4);
            } else if (bulletMode == 5) {
                 anim.SetInteger("AnimState", 5);
            }

            timer = 0f;
        }

        if (!playerSpr.enabled) {
            Reset();
        }
    }

    private void openDoor() {
        door.SetActive(false);
    }

    private void playSound(int sound) {
        if (sound == 0) {
            if (!roarSound.isPlaying)
                roarSound.Play();
        } else if (sound == 1) {
            if (!hitSound.isPlaying)
                hitSound.Play();
        } else if (sound == 2) {
            if (!shootSound.isPlaying)
                shootSound.Play();
        }
    }

    private void Reset() {
        bulletMode = 0;
        bossTimer = 0;
        dSpikesAnim.SetInteger("spikeMode", 0);
        uSpikesAnim.SetInteger("spikeMode", 0);
    }

    private Color ChangeColor() {
        if (currentColor == 0) {
            return red;
        } else if (currentColor == 1) {
            return blue;
        } else if (currentColor == 2) {
            return green;
        } else {
            return yellow;
        }
    }

    public void shakeCamera() {
        camShake.Shake(0.1f, 0.3f);
    }

    public void moveUpSpike() {
        uSpikesAnim.SetInteger("spikeMode", 1);
        StartCoroutine(moveUpSpikeBack(1.25f));
    }

    public IEnumerator moveUpSpikeBack(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        uSpikesAnim.SetInteger("spikeMode", 2);
        yield return null;
    }

    public void moveDownSpike() {
        dSpikesAnim.SetInteger("spikeMode", 2);
        StartCoroutine(moveDownSpikeBack(1.25f));
    }

    public IEnumerator moveDownSpikeBack(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        dSpikesAnim.SetInteger("spikeMode", 1);
        yield return null;
    }

    private void IBullets() {

        bulletSpeed = Random.Range(5f, 6f);
        bulletDelay = Random.Range(0.1f, 0.15f);
        destroyTime = 2.2f;
        playSound(2);

        for (int i = 0; i < bulletEmitter.Length; i++) {

            if (bulletEmitter[i].transform.rotation.z != 0) {
                bulletEmitter[i].transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            GameObject bulletInstanceOne = Instantiate(bullet, bulletEmitter[i].transform.position, bulletEmitter[i].transform.rotation) as GameObject;
            GameObject bulletInstanceTwo = Instantiate(bullet, bulletEmitter[i].transform.position, bulletEmitter[i].transform.rotation) as GameObject;

            Rigidbody2D bulletOneBody;
            SpriteRenderer bulletOneRend;
            bulletOneBody = bulletInstanceOne.GetComponent<Rigidbody2D>();
            bulletOneRend = bulletInstanceOne.GetComponent<SpriteRenderer>();

            bulletOneRend.color = ChangeColor();
            bulletOneBody.velocity = bulletEmitter[i].transform.up * bulletSpeed;
            Destroy(bulletInstanceOne, destroyTime);

            Rigidbody2D bulletTwoBody;
            SpriteRenderer bulletTwoRend;
            bulletTwoBody = bulletInstanceTwo.GetComponent<Rigidbody2D>();
            bulletTwoRend = bulletInstanceTwo.GetComponent<SpriteRenderer>();

            bulletTwoRend.color = ChangeColor();
            bulletTwoBody.velocity = -bulletEmitter[i].transform.up * bulletSpeed;
            Destroy(bulletInstanceTwo, destroyTime);
        }  
    }

    private void XBullets() {

        bulletSpeed = 4.5f;
        bulletDelay = Random.Range(0.2f, 0.5f);
        destroyTime = 2.5f;
        playSound(2);

        for (int i = 0; i < bulletEmitter.Length; i++) {
            GameObject bulletInstanceOne = Instantiate(bullet, bulletEmitter[i].transform.position, bulletEmitter[i].transform.rotation) as GameObject;
            GameObject bulletInstanceTwo = Instantiate(bullet, bulletEmitter[i].transform.position, bulletEmitter[i].transform.rotation) as GameObject;
            GameObject bulletInstanceThree = Instantiate(bullet, bulletEmitter[i].transform.position, bulletEmitter[i].transform.rotation) as GameObject;
            GameObject bulletInstanceFour = Instantiate(bullet, bulletEmitter[i].transform.position, bulletEmitter[i].transform.rotation) as GameObject;

            Rigidbody2D bulletOneBody;
            SpriteRenderer bulletOneRend;
            bulletOneBody = bulletInstanceOne.GetComponent<Rigidbody2D>();
            bulletOneRend = bulletInstanceOne.GetComponent<SpriteRenderer>();

            bulletOneRend.color = ChangeColor();
            bulletOneBody.velocity = (bulletEmitter[i].transform.up + bulletEmitter[i].transform.right).normalized * bulletSpeed;
            Destroy(bulletInstanceOne, destroyTime);

            Rigidbody2D bulletTwoBody;
            SpriteRenderer bulletTwoRend;
            bulletTwoBody = bulletInstanceTwo.GetComponent<Rigidbody2D>();
            bulletTwoRend = bulletInstanceTwo.GetComponent<SpriteRenderer>();

            bulletTwoRend.color = ChangeColor();
            bulletTwoBody.velocity = (bulletEmitter[i].transform.up - bulletEmitter[i].transform.right).normalized * bulletSpeed;
            Destroy(bulletInstanceTwo, destroyTime);

            Rigidbody2D bulletThreeBody;
            SpriteRenderer bulletThreeRend;
            bulletThreeBody = bulletInstanceThree.GetComponent<Rigidbody2D>();
            bulletThreeRend = bulletInstanceThree.GetComponent<SpriteRenderer>();

            bulletThreeRend.color = ChangeColor();
            bulletThreeBody.velocity = (-bulletEmitter[i].transform.up + bulletEmitter[i].transform.right).normalized * bulletSpeed;
            Destroy(bulletInstanceThree, destroyTime);

            Rigidbody2D bulletFourBody;
            SpriteRenderer bulletFourRend;
            bulletFourBody = bulletInstanceFour.GetComponent<Rigidbody2D>();
            bulletFourRend = bulletInstanceFour.GetComponent<SpriteRenderer>();

            bulletFourRend.color = ChangeColor();
            bulletFourBody.velocity = (-bulletEmitter[i].transform.up - bulletEmitter[i].transform.right).normalized * bulletSpeed;
            Destroy(bulletInstanceFour, destroyTime);
        }
    }

}
