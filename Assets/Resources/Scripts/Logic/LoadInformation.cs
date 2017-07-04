using UnityEngine;
using System.Collections;

public class LoadInformation : PlayerManager {

    public void LoadAllInformation() {
        Vector3 temp;
        temp.x = PlayerPrefs.GetFloat("PLAYERPOSX");
        temp.y = PlayerPrefs.GetFloat("PLAYERPOSY");
        temp.z = PlayerPrefs.GetFloat("PLAYERPOSZ");
        spawnLocation.position = temp;

        int colorTemp;
        colorTemp = PlayerPrefs.GetInt("COLOR");

        if (colorTemp == 0) {
            sprite.color = red;
        } else if (colorTemp == 1) {
            sprite.color = blue;
        } else if (colorTemp == 2) {
            sprite.color = green;
        } else if (colorTemp == 3) {
            sprite.color = yellow;
        }
    }
}
