using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFight : MonoBehaviour {

    public GameObject[] bulletEmitter;
    public GameObject bullet;
    public float bulletSpeed;
    public float bulletDelay;
    public int bulletMode = 0; // 0 = I, 1 = X
    public int colorMode = 0; // 0 = RED, 1 = BLUE, 2 = GREEN, 3 = YELLOW

    private float timer;
    private Animator anim;
    private Renderer rend;

    private int currentColor;
    private Color yellow = new Color(0.898f, 0.785f, 0.102f);
    private Color green = new Color(0.145f, 0.785f, 0.102f);
    private Color red = new Color(0.784f, 0.102f, 0.149f);
    private Color blue = new Color(0.129f, 0.165f, 0.986f);

    private void Awake() {
        anim = GetComponent<Animator>();
        rend = GetComponent<Renderer>();
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

    private void IBullets() {
        for (int i = 0; i < bulletEmitter.Length; i++) {
            GameObject bulletInstanceOne = Instantiate(bullet, bulletEmitter[i].transform.position, bulletEmitter[i].transform.rotation) as GameObject;
            GameObject bulletInstanceTwo = Instantiate(bullet, bulletEmitter[i].transform.position, bulletEmitter[i].transform.rotation) as GameObject;

            Rigidbody2D bulletOneBody;
            SpriteRenderer bulletOneRend;
            bulletOneBody = bulletInstanceOne.GetComponent<Rigidbody2D>();
            bulletOneRend = bulletInstanceOne.GetComponent<SpriteRenderer>();

            bulletOneRend.color = ChangeColor();
            bulletOneBody.velocity = bulletEmitter[i].transform.up * bulletSpeed;
            Destroy(bulletInstanceOne, 2.0f);

            Rigidbody2D bulletTwoBody;
            SpriteRenderer bulletTwoRend;
            bulletTwoBody = bulletInstanceTwo.GetComponent<Rigidbody2D>();
            bulletTwoRend = bulletInstanceTwo.GetComponent<SpriteRenderer>();

            bulletTwoRend.color = ChangeColor();
            bulletTwoBody.velocity = -bulletEmitter[i].transform.up * bulletSpeed;
            Destroy(bulletInstanceTwo, 2.0f);
        }  
    }

    private void XBullets() {
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
            Destroy(bulletInstanceOne, 2.0f);

            Rigidbody2D bulletTwoBody;
            SpriteRenderer bulletTwoRend;
            bulletTwoBody = bulletInstanceTwo.GetComponent<Rigidbody2D>();
            bulletTwoRend = bulletInstanceTwo.GetComponent<SpriteRenderer>();

            bulletTwoRend.color = ChangeColor();
            bulletTwoBody.velocity = (bulletEmitter[i].transform.up - bulletEmitter[i].transform.right).normalized * bulletSpeed;
            Destroy(bulletInstanceTwo, 2.0f);

            Rigidbody2D bulletThreeBody;
            SpriteRenderer bulletThreeRend;
            bulletThreeBody = bulletInstanceThree.GetComponent<Rigidbody2D>();
            bulletThreeRend = bulletInstanceThree.GetComponent<SpriteRenderer>();

            bulletThreeRend.color = ChangeColor();
            bulletThreeBody.velocity = (-bulletEmitter[i].transform.up + bulletEmitter[i].transform.right).normalized * bulletSpeed;
            Destroy(bulletInstanceThree, 2.0f);

            Rigidbody2D bulletFourBody;
            SpriteRenderer bulletFourRend;
            bulletFourBody = bulletInstanceFour.GetComponent<Rigidbody2D>();
            bulletFourRend = bulletInstanceFour.GetComponent<SpriteRenderer>();

            bulletFourRend.color = ChangeColor();
            bulletFourBody.velocity = (-bulletEmitter[i].transform.up - bulletEmitter[i].transform.right).normalized * bulletSpeed;
            Destroy(bulletInstanceFour, 2.0f);
        }
    }

}
