﻿using UnityEngine;

public class SaveInformation : PlayerManager {

    public void SavePosInfromation() {
        PlayerPrefs.SetFloat("PLAYERPOSX", spawnLocation.position.x);
        PlayerPrefs.SetFloat("PLAYERPOSY", spawnLocation.position.y);
        PlayerPrefs.SetFloat("PLAYERPOSZ", spawnLocation.position.z);
    }

    public void SaveColorChanging(int value) {
        PlayerPrefs.SetInt("COLOR", value);
    }

    public void SaveGravityChanging(int value) {
        PlayerPrefs.SetInt("GRAVITY", value);
    }

    public void SaveScene(int value) {
        PlayerPrefs.SetInt("SCENE", value);
    }
}
