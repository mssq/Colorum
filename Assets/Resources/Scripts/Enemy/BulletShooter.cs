using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooter : MonoBehaviour {

    public GameObject bulletEmitter;
    public GameObject bullet;
    public float bulletSpeed;
    public float bulletDelay;

    private float timer;

    private void Update() {
        timer += Time.deltaTime;

        if (timer > bulletDelay) {
            GameObject bulletInstance = Instantiate(bullet, transform.position, transform.rotation) as GameObject;

            Rigidbody2D tempBody;
            tempBody = bulletInstance.GetComponent<Rigidbody2D>();

            tempBody.velocity = transform.up * bulletSpeed;
            Destroy(bulletInstance, 2.0f);

            timer = 0f;
        }
    }

}
