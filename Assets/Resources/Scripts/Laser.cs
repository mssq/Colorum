using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : PlayerManager {

    private SpriteRenderer laserSprite;

    protected override void Awake() {
        base.Awake();
        laserSprite = this.gameObject.GetComponent<SpriteRenderer>();
    }

    private void Start () {
		if (this.gameObject.name.Equals("Laser Red")) {
            laserSprite.color = red;
        } else if (this.gameObject.name.Equals("Laser Blue")) {
            laserSprite.color = blue;
        } else if (this.gameObject.name.Equals("Laser Green")) {
            laserSprite.color = green;
        } else if (this.gameObject.name.Equals("Laser Yellow")) {
            laserSprite.color = yellow;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Change Red") {
            laserSprite.color = red;
        } else if (collision.tag == "Change Blue") {
            laserSprite.color = blue;
        } else if (collision.tag == "Change Green") {
            laserSprite.color = green;
        } else if (collision.tag == "Change Yellow") {
            laserSprite.color = yellow;
        }
    }
}
