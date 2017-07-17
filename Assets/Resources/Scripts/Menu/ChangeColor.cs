using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour {

    private SpriteRenderer spr;
    private int currentColor = 0;

    private Color yellow = new Color(0.898f, 0.785f, 0.102f);
    private Color green = new Color(0.145f, 0.785f, 0.102f);
    private Color red = new Color(0.784f, 0.102f, 0.149f);
    private Color blue = new Color(0.129f, 0.165f, 0.986f);

    private void Awake() {

        spr = this.gameObject.GetComponent<SpriteRenderer>();
    }

    private void nextColor() {
        currentColor++;

        if (currentColor == 4) {
            currentColor = 0;
        }
        
        switch (currentColor) {
            case 0:
                spr.color = red;
                break;
            case 1:
                spr.color = blue;
                break;
            case 2:
                spr.color = green;
                break;
            case 3:
                spr.color = yellow;
                break;
        }
    }
}
