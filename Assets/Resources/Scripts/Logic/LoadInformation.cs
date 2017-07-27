using UnityEngine;
using UnityEngine.UI;

public class LoadInformation : PlayerManager {

    public Image gravityImage;
    public Image colorImage;

    public void LoadAllInformation() {

        Vector3 temp = new Vector3(PlayerPrefs.GetFloat("PLAYERPOSX"), PlayerPrefs.GetFloat("PLAYERPOSY"),
            PlayerPrefs.GetFloat("PLAYERPOSZ"));
        if (temp.x == 0 && temp.y == 0) {
            temp = new Vector3(-12f, 1f, 0f);
        }
        spawnLocation.position = temp;

        int colorTemp = PlayerPrefs.GetInt("COLOR");
        chooserInput.setColorState(colorTemp);
        if (colorTemp == 0) {
            sprite.color = red;
            colorImage.sprite = Resources.Load<Sprite>("Sprites/PlayerColorRed");
        } else if (colorTemp == 1) {
            sprite.color = blue;
            colorImage.sprite = Resources.Load<Sprite>("Sprites/PlayerColorBlue");
        } else if (colorTemp == 2) {
            sprite.color = green;
            colorImage.sprite = Resources.Load<Sprite>("Sprites/PlayerColorGreen");
        } else if (colorTemp == 3) {
            sprite.color = yellow;
            colorImage.sprite = Resources.Load<Sprite>("Sprites/PlayerColorYellow");
        }

        int gravity = PlayerPrefs.GetInt("GRAVITY");
        chooserInput.setGravityState(gravity);
        if (gravity == 0) {
            playerScript.SetGravity(3);
            gravityImage.sprite = Resources.Load<Sprite>("Sprites/GravityIconLow");
        } else if (gravity == 1) {
            playerScript.SetGravity(5.5f);
            gravityImage.sprite = Resources.Load<Sprite>("Sprites/GravityIconMed");
        } else if (gravity == 2) {
            playerScript.SetGravity(8f);
            gravityImage.sprite = Resources.Load<Sprite>("Sprites/GravityIconHigh");
        }
    }
}
