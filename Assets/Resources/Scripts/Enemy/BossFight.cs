using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFight : MonoBehaviour {

    public GameObject[] bulletEmitter;
    public GameObject bullet;
    public float bulletSpeed;
    public float bulletDelay;
    public int bulletMode = 0; // 0 = I, 1 = X
    public int currentColor = 0; // 0 = RED, 1 = BLUE, 2 = GREEN, 3 = YELLOW
    public float destroyTime = 2.5f;
    public GameObject dSpikes;

    private float timer;
    private Vector2 dTarget;
    private Vector2 spikeOrigPos;
    private bool moveSpikes = false;
    private Animator anim;
    private Renderer rend;

    private Color yellow = new Color(0.898f, 0.785f, 0.102f);
    private Color green = new Color(0.145f, 0.785f, 0.102f);
    private Color red = new Color(0.784f, 0.102f, 0.149f);
    private Color blue = new Color(0.129f, 0.165f, 0.986f);

    private void Awake() {
        anim = GetComponent<Animator>();
        rend = GetComponent<Renderer>();
    }

    private void Start() {
        spikeOrigPos = new Vector2(dSpikes.transform.position.x, dSpikes.transform.position.y);
        dTarget = new Vector2(dSpikes.transform.position.x, dSpikes.transform.position.y + 0.25f);
    }

    private void Update() {
        timer += Time.deltaTime;

        if (timer > bulletDelay) {

            currentColor = Random.Range(0, 4);
            rend.material.SetColor("_Colorout", ChangeColor());

            if (bulletMode == 0) {
                anim.SetInteger("AnimState", 1);
                IBullets();
            } else if (bulletMode == 1) {
                anim.SetInteger("AnimState", 2);
                XBullets();
            } else if (bulletMode == 2) {
                anim.SetInteger("AnimState", 3);
                print("MOVESPIKES : " + moveSpikes);
                if (moveSpikes) {
                    if (dSpikes.transform.position.y < dTarget.y) {
                        dSpikes.transform.position = Vector3.MoveTowards(dSpikes.transform.position, dTarget, 0.05f);
                    } else {
                        StartCoroutine(moveDownSpikeBack(1.25f));
                    }
                } else if (!moveSpikes) {
                    dSpikes.transform.position = Vector3.MoveTowards(dSpikes.transform.position, spikeOrigPos, 0.05f);
                }
                
            }

            timer = 0f;
        }
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

    public void moveDownSpike() {
        moveSpikes = true;
    }

    public IEnumerator moveDownSpikeBack(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        moveSpikes = false;
        yield return null;
    }

    private void IBullets() {

        bulletSpeed = Random.Range(5f, 6f);
        bulletDelay = Random.Range(0.1f, 0.15f);

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
        bulletDelay = Random.Range(0.2f, 0.25f);
        destroyTime = 2f;

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
